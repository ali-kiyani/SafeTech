import { Component, OnInit, Injector, Input } from '@angular/core';
import { AppComponentBase } from '@shared/app-component-base';
import { BsModalRef } from 'ngx-bootstrap/modal';
import { RequestServiceProxy, RequestDto, RequestDetailsDto } from '@shared/service-proxies/service-proxies';

@Component({
  selector: 'app-request-details',
  templateUrl: './request-details.component.html',
  styleUrls: ['./request-details.component.css']
})
export class RequestDetailsComponent extends AppComponentBase
implements OnInit {

  request: RequestDto;
  requestDetails = new RequestDetailsDto();
  loaded = false;

  constructor( injector: Injector,
    private _requestService: RequestServiceProxy,
    public bsModalRef: BsModalRef) {
      super(injector);
     }

  ngOnInit(): void {
    this._requestService.getRequestDetails(this.request.id)
    .subscribe((result: RequestDetailsDto) => {
      this.requestDetails = result;
      this.loaded = true;
    });
  }

}
