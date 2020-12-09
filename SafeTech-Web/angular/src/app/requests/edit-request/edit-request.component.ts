import { Component, OnInit, Injector, EventEmitter, Output } from '@angular/core';
import { RequestDto, RequestServiceProxy } from '@shared/service-proxies/service-proxies';
import { AppComponentBase } from '@shared/app-component-base';
import { BsModalRef } from 'ngx-bootstrap/modal';
import { finalize } from 'rxjs/operators';

@Component({
  selector: 'app-edit-request',
  templateUrl: './edit-request.component.html',
  styleUrls: ['./edit-request.component.css']
})
export class EditRequestComponent extends AppComponentBase
implements OnInit {

  saving = false;
  id: number;
  request = new RequestDto();

  @Output() onSave = new EventEmitter<any>();

  constructor( injector: Injector,
    private _requestService: RequestServiceProxy,
    public bsModalRef: BsModalRef) {
      super(injector);
  }

  ngOnInit(): void {
    this._requestService
    .get(this.id)
    .subscribe((result: RequestDto) => {
      this.request = result;
    });
  }

  save() {
    this.saving = true;

    const request = new RequestDto();
    request.init(this.request);

    this._requestService
      .update(request)
      .pipe(
        finalize(() => {
          this.saving = false;
        })
      )
      .subscribe(() => {
        this.notify.info(this.l('SavedSuccessfully'));
        this.bsModalRef.hide();
        this.onSave.emit();
      });
  }
}
