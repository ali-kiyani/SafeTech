import { Component, OnInit, Injector } from '@angular/core';
import { appModuleAnimation } from '@shared/animations/routerTransition';
import { PagedRequestDto, PagedListingComponentBase } from '@shared/paged-listing-component-base';
import { RequestDto, RequestServiceProxy, RequestDtoPagedResultDto, CitiesDto, InstallationsServiceProxy } from '@shared/service-proxies/service-proxies';
import { finalize } from 'rxjs/operators';
import { BsModalRef, BsModalService } from 'ngx-bootstrap/modal';
import { CreateRequestComponent } from './create-request/create-request.component';
import { EditRequestComponent } from './edit-request/edit-request.component';
import { RequestDetailsComponent } from './request-details/request-details.component';
import { ActivatedRoute } from '@angular/router';

class PagedRequestsRequestDto extends PagedRequestDto {
  keyword: string;
}

@Component({
  selector: 'app-requests',
  templateUrl: './requests.component.html',
  animations: [appModuleAnimation()]
})
export class RequestsComponent extends PagedListingComponentBase<RequestDto> implements OnInit {

  requests: RequestDto[] = [];
  cities: CitiesDto[] = [];
  keyword = '';
  status: string;
  selectedCity = 0;

  constructor(injector: Injector, private _requestService: RequestServiceProxy, private _modalService: BsModalService,
    private route: ActivatedRoute, private _installationService: InstallationsServiceProxy) {
    super(injector);
    this.route.params.subscribe(param => {
      this.status = param.status;
      this.refresh();
    });
  }

  ngOnInit() {
    this._installationService.getAllCities().subscribe(result => {
      this.cities = result.items;
    });
  }

  list(request: PagedRequestsRequestDto, pageNumber: number, finishedCallback: Function): void {
    request.keyword = this.keyword;
    this._requestService
      .getAll(request.keyword, this.status, this.selectedCity, request.skipCount, request.maxResultCount)
      .pipe(
        finalize(() => {
          finishedCallback();
        })
      )
      .subscribe((result: RequestDtoPagedResultDto) => {
        this.requests = result.items;
        this.showPaging(result, pageNumber);
      });
  }
  delete(request: RequestDto): void {
    abp.message.confirm(
      this.l('RequestDeleteWarningMessage', request.name),
      undefined,
      (result: boolean) => {
        if (result) {
          this._requestService
            .delete(request.id)
            .pipe(
              finalize(() => {
                abp.notify.success(this.l('SuccessfullyDeleted'));
                this.refresh();
              })
            )
            .subscribe(() => { });
        }
      }
    );
  }

  listCityChange() {
    this.refresh();
  }

  createRequest() {
    this.showCreateOrEditRequestDialog();
  }

  editRequest(request: RequestDto) {
    this.showCreateOrEditRequestDialog(request.id);
  }

  showCreateOrEditRequestDialog(id?: number): void {
    let createOrEditRequestDialog: BsModalRef;
    if (!id) {
      createOrEditRequestDialog = this._modalService.show(
        CreateRequestComponent,
        {
          class: 'modal-lg',
        }
      );
    } else {
      createOrEditRequestDialog = this._modalService.show(
        EditRequestComponent,
        {
          class: 'modal-lg',
          initialState: {
            id: id,
          },
        }
      );
    }
    createOrEditRequestDialog.content.onSave.subscribe(() => {
      this.refresh();
    });
  }

  approveRequest(request: RequestDto)
  {
    abp.message.confirm(
      this.l('RequestApproveWarningMessage', 'R#' + request.id),
      undefined,
      (result: boolean) => {
        if (result) {
          this._requestService
            .approveRequest(request.id)
            .pipe(
              finalize(() => {
                abp.notify.success(this.l('ApprovedSuccessfully'));
                this.refresh();
              })
            )
            .subscribe(() => { });
        }
      }
    );
  }

  declineRequest(request: RequestDto)
  {
    abp.message.confirm(
      this.l('RequestDeclineWarningMessage', 'R#' + request.id),
      undefined,
      (result: boolean) => {
        if (result) {
          this._requestService
            .declineRequest(request.id)
            .pipe(
              finalize(() => {
                abp.notify.success(this.l('DeclinedSuccessfully'));
                this.refresh();
              })
            )
            .subscribe(() => { });
        }
      }
    );
  }

  getDetails(request: RequestDto) {
    this._modalService.show(
      RequestDetailsComponent,
      {
        class: 'modal-lg',
        initialState: {
          request: request
        }
      }
    );
  }
}
