import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { StageService } from 'src/app/services/stage.service';
import { Stage } from 'src/entities/stage';
import { FacadeService } from 'src/app/services/facade.service';

@Component({
  selector: 'app-stage-detail',
  templateUrl: './stage-detail.component.html',
  styleUrls: ['./stage-detail.component.css']
})

export class StageDetailComponent implements OnInit {

  constructor(private route: ActivatedRoute, private api: FacadeService, private router: Router) { }

  stage: Stage = null;
  processId: number;

  ngOnInit() {
    this.getStageByID(this.route.snapshot.params['id']);
  }

  getStageByID(id) {
    this.api.stageService.getByID<Stage>(id)
            .subscribe(res => {
              this.stage = res;
              this.processId = this.stage.processId;
              console.log(res);
              
            }, err => {
              console.log(err);
            })
  }
}
