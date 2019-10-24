import { Stage } from './stage';
import { Consultant } from './consultant';
import { Candidate } from './candidate';
import { ProcessStatusEnum } from './enums/process-status.enum';
import { SeniorityEnum } from './enums/seniority.enum';
import { HrStage } from './hr-stage';
import { TechnicalStage } from './technical-stage';
import { ClientStage } from './client-stage';
import { OfferStage } from './offer-stage';
import { EnglishLevelEnum } from './enums/english-level.enum';

export class Process {
    id: number;
    startDate: Date;
    endDate: Date;
    status: ProcessStatusEnum;
    candidateId: number;
    candidate: Candidate;
    consultantOwnerId: number;
    consultantOwner: Consultant;
    consultantDelegateId: number;
    consultantDelegate: Consultant;
    rejectionReason: string;
    actualSalary: number;
    wantedSalary: number;
    agreedSalary: number;
    englishLevel: EnglishLevelEnum;
    seniority: SeniorityEnum;
    hrStage: HrStage;
    technicalStage: TechnicalStage;
    clientStage: ClientStage;
    offerStage: OfferStage;
}
