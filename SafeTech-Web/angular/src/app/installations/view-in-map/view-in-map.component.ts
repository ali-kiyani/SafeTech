import { Component, Injector, OnInit } from '@angular/core';
import { AppComponentBase } from '@shared/app-component-base';
import { InstallationsDto } from '@shared/service-proxies/service-proxies';
import { BsModalRef } from 'ngx-bootstrap/modal';

@Component({
  selector: 'app-view-in-map',
  templateUrl: './view-in-map.component.html',
  styleUrls: ['./view-in-map.component.css']
})
export class ViewInMapComponent extends AppComponentBase implements OnInit {

  installation: InstallationsDto;
  constructor(injector: Injector,
    public bsModalRef: BsModalRef) {
      super(injector);
  }

  ngOnInit(): void {
  }

}
