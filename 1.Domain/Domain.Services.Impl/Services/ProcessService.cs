﻿using AutoMapper;
using Core.Persistance;
using Domain.Model;
using Domain.Model.Enum;
using Domain.Services.Contracts.Process;
using Domain.Services.Contracts.Stage;
using Domain.Services.Interfaces.Repositories;
using Domain.Services.Interfaces.Services;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Domain.Services.Impl.Services
{
    public class ProcessService : IProcessService
    {
        private readonly IMapper _mapper;
        private readonly IProcessRepository _processRepository;
        private readonly IProcessStageRepository _processStageRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IRepository<Consultant> _consultantRepository;
        private readonly IRepository<Community> _communityRepository;
        private readonly IRepository<CandidateProfile> _candidateProfileRepository;
        private readonly IRepository<Candidate> _candidateRepository;
        private readonly IRepository<Office> _officeRepository;
        private readonly IHrStageRepository _hrStageRepository;
        private readonly ITechnicalStageRepository _technicalStageRepository;
        private readonly IClientStageRepository _clientStageRepository;
        private readonly IOfferStageRepository _offerStageRepository;

        public ProcessService(IMapper mapper,
            IRepository<Consultant> consultantRepository,
            IRepository<Candidate> candidateRepository,
            IRepository<CandidateProfile> candidateProfileRepository,
            IRepository<Community> communityRepository,
            IRepository<Office> officeRepository,
            IProcessRepository processRepository,
            IProcessStageRepository processStageRepository,
            IHrStageRepository hrStageRepository,
            ITechnicalStageRepository technicalStageRepository,
            IClientStageRepository clientStageRepository,
            IOfferStageRepository offerStageRepository,
            IUnitOfWork unitOfWork)
        {
            _consultantRepository = consultantRepository;
            _candidateRepository = candidateRepository;
            _candidateProfileRepository = candidateProfileRepository;
            _communityRepository = communityRepository;
            _officeRepository = officeRepository;
            _mapper = mapper;
            _processRepository = processRepository;
            _processStageRepository = processStageRepository;
            _hrStageRepository = hrStageRepository;
            _technicalStageRepository = technicalStageRepository;
            _clientStageRepository = clientStageRepository;
            _offerStageRepository = offerStageRepository;
            _unitOfWork = unitOfWork;
        }

        public ReadedProcessContract Read(int id)
        {
            var process = _processRepository
                .QueryEager().SingleOrDefault(_ => _.Id == id);

            return _mapper.Map<ReadedProcessContract>(process);
        }

        public void Delete(int id)
        {
            var process = _processRepository.GetByIdFullProcess(id);

            _processRepository.Delete(process);

            _unitOfWork.Complete();
        }

        public IEnumerable<ReadedProcessContract> List()
        {
            var candidateQuery = _processRepository
                .QueryEager();

            var candidateResult = candidateQuery.ToList();

            return _mapper.Map<List<ReadedProcessContract>>(candidateResult);
        }

        public CreatedProcessContract Create(CreateProcessContract createProcessContract)
        {
            var process = _mapper.Map<Process>(createProcessContract);
            //var candidate = _mapper.Map<Candidate>(createProcessContract.Candidate);
            _candidateRepository.Update(process.Candidate);

            //process.Candidate = candidate;

            //var candidate = _candidateRepository.QueryEager().FirstOrDefault(c => c.Id == process.Candidate.Id);

            //var updatedCandidate = _candidateRepository.Update(candidate);

            //process.Candidate = updatedCandidate;
            //process.CandidateId = updatedCandidate.Id;

            this.AddRecruiterToCandidate(process.Candidate, createProcessContract.Candidate.Recruiter);
            this.AddCommunityToCandidate(process.Candidate, createProcessContract.Candidate.Community);
            this.AddCandidateProfileToCandidate(process.Candidate, createProcessContract.Candidate.Profile);
            this.AddOfficeToCandidate(process.Candidate, createProcessContract.Candidate.PreferredOfficeId);
            var createdProcess = _processRepository.Create(process);

            _unitOfWork.Complete();

            var createdProcessContract = _mapper.Map<CreatedProcessContract>(createdProcess);

            return createdProcessContract;
        }

        private void AddCandidateProfileToCandidate(Candidate candidate, int profileID)
        {
            var profile = _candidateProfileRepository.Query().Where(_ => _.Id == profileID).FirstOrDefault();
            if (profile == null)
                throw new Domain.Model.Exceptions.Consultant.ConsultantNotFoundException(profileID);

            candidate.Profile = profile;
        }

        private void AddCommunityToCandidate(Candidate candidate, int communityID)
        {
            var community = _communityRepository.Query().Where(_ => _.Id == communityID).FirstOrDefault();
            if (community == null)
                throw new Domain.Model.Exceptions.Consultant.ConsultantNotFoundException(communityID);

            candidate.Community = community;
        }

        private void AddRecruiterToCandidate(Candidate candidate, int recruiterID)
        {

            var recruiter = _consultantRepository.Query().Where(_ => _.Id == recruiterID).FirstOrDefault();
            if (recruiter == null)
                throw new Domain.Model.Exceptions.Consultant.ConsultantNotFoundException(recruiterID);

            candidate.Recruiter = recruiter;
        }

        private void AddOfficeToCandidate(Candidate candidate, int officeId)
        {

            var office = _officeRepository.Query().Where(_ => _.Id == officeId).FirstOrDefault();
            if (office == null)
                throw new Domain.Model.Exceptions.Office.OfficeNotFoundException(officeId);

            candidate.PreferredOffice = office;
        }

        public void Update(UpdateProcessContract updateProcessContract)
        {
            var process = _mapper.Map<Process>(updateProcessContract);
            process.Status = SetProcessStatus(process);
            process.Candidate.EnglishLevel = process.HrStage.EnglishLevel;
            process.Candidate.Status = SetCandidateStatus(process.Status);
            var updatedCandidate = _candidateRepository.Update(process.Candidate);


            _hrStageRepository.Update(process.HrStage);
            _technicalStageRepository.Update(process.TechnicalStage);
            _clientStageRepository.Update(process.ClientStage);
            _offerStageRepository.Update(process.OfferStage);


            this.AddRecruiterToCandidate(process.Candidate, updateProcessContract.Candidate.Recruiter);
            this.AddCommunityToCandidate(process.Candidate, updateProcessContract.Candidate.Community);
            this.AddCandidateProfileToCandidate(process.Candidate, updateProcessContract.Candidate.Profile);
            this.AddOfficeToCandidate(process.Candidate, updateProcessContract.Candidate.PreferredOfficeId);

            var updatedProcess = _processRepository.Update(process);

            _unitOfWork.Complete();
        }

        public void Approve(int processID)
        {
            _processRepository.Approve(processID);
            _unitOfWork.Complete();
        }

        public void Reject(int id, string rejectionReason)
        {
            _processRepository.Reject(id, rejectionReason);
            _unitOfWork.Complete();
        }

        public ProcessStatus SetProcessStatus(Process process)
        {
            switch (process.OfferStage.Status)
            {
                case StageStatus.NA:
                    switch (process.ClientStage.Status)
                    {
                        case StageStatus.NA:
                            switch (process.TechnicalStage.Status)
                            {
                                case StageStatus.NA:
                                    switch (process.HrStage.Status)
                                    {
                                        case StageStatus.NA:
                                            return ProcessStatus.NA;
                                        case StageStatus.InProgress:
                                            return ProcessStatus.InProgress;
                                        case StageStatus.Accepted:
                                            return ProcessStatus.InProgress;
                                        case StageStatus.Declined:
                                            return ProcessStatus.Declined;
                                        case StageStatus.Rejected:
                                            return ProcessStatus.Rejected;
                                        case StageStatus.Hired:
                                            return ProcessStatus.Hired;
                                        default:
                                            return ProcessStatus.NA;
                                    }
                                case StageStatus.InProgress:
                                    return ProcessStatus.InProgress;
                                case StageStatus.Accepted:
                                    return ProcessStatus.InProgress;
                                case StageStatus.Declined:
                                    return ProcessStatus.Declined;
                                case StageStatus.Rejected:
                                    return ProcessStatus.Rejected;
                                case StageStatus.Hired:
                                    return ProcessStatus.Hired;
                                default:
                                    return ProcessStatus.NA;
                            }
                        case StageStatus.InProgress:
                            return ProcessStatus.InProgress;
                        case StageStatus.Accepted:
                            return ProcessStatus.InProgress;
                        case StageStatus.Declined:
                            return ProcessStatus.Declined;
                        case StageStatus.Rejected:
                            return ProcessStatus.Rejected;
                        default:
                            return ProcessStatus.NA;
                    }
                case StageStatus.InProgress:
                    return ProcessStatus.InProgress;
                case StageStatus.Accepted:
                    return ProcessStatus.OfferAccepted;
                case StageStatus.Declined:
                    return ProcessStatus.Declined;
                case StageStatus.Rejected:
                    return ProcessStatus.Rejected;
                case StageStatus.Hired:
                    return ProcessStatus.Hired;
                default:
                    return ProcessStatus.NA;
            }
        }

        public CandidateStatus SetCandidateStatus(ProcessStatus processStatus)
        {
            switch (processStatus)
            {
                case ProcessStatus.NA:
                    return CandidateStatus.New;
                case ProcessStatus.InProgress:
                    return CandidateStatus.InProgress;
                case ProcessStatus.Recall:
                    return CandidateStatus.Recall;
                case ProcessStatus.Hired:
                    return CandidateStatus.Hired;
                case ProcessStatus.Rejected:
                    return CandidateStatus.Rejected;
                case ProcessStatus.Declined:
                    return CandidateStatus.Rejected;
                case ProcessStatus.OfferAccepted:
                    return CandidateStatus.InProgress;
                default:
                    return CandidateStatus.New;
            }
        }
    }
}
