import { AfterViewInit, Component, EventEmitter, Injector, OnInit } from '@angular/core';
import { appModuleAnimation } from '@shared/animations/routerTransition';
import { PagedListingComponentBase, PagedRequestDto } from '@shared/paged-listing-component-base';
import { CitiesDto, InstallationsDto, InstallationsDtoPagedResultDto, InstallationsServiceProxy } from '@shared/service-proxies/service-proxies';
import { debug } from 'console';
import { BsModalRef, BsModalService } from 'ngx-bootstrap/modal';
import { finalize } from 'rxjs/operators';
import { isContext } from 'vm';
import { CreateInstallationComponent } from './create-installation/create-installation.component';
import { EditInstallationComponent } from './edit-installation/edit-installation.component';
import { ViewInMapComponent } from './view-in-map/view-in-map.component';

class PagedInstallationInstallationDto extends PagedRequestDto {
  keyword: string;
}

@Component({
  selector: 'app-installations',
  templateUrl: './installations.component.html',
  styleUrls: ['./installations.component.css'],
  animations: [appModuleAnimation()]
})
export class InstallationsComponent extends PagedListingComponentBase<InstallationsDto> implements OnInit, AfterViewInit {

  installations: InstallationsDto[] = [];
  keyword = '';
  status: string;
  cities: CitiesDto[] = [];
  selectedCity = 0;
  tabClick = new EventEmitter<any>();
  lat = 33.687452;
  lng = 73.015991;

  constructor(injector: Injector, private _modalService: BsModalService, private _installationService: InstallationsServiceProxy) {
    super(injector);
    this.refresh();
    this.tabClick.subscribe(result => {
      if (result === 'List') {
        this.refresh();
      } else if (result === 'Map') {
        this.installationsForCity();
      }
    });
  }

  ngOnInit() {
    this._installationService.getAllCities().subscribe(result => {
      this.cities = result.items;
    });
  }

  installationsForCity() {
    debugger;
    this._installationService.getInstallationsForCity(this.selectedCity).subscribe(installations => {
      this.installations = installations.items;
      if (this.installations.length > 0) {
        this.lat = this.installations[0].lat;
        this.lng = this.installations[0].lng;
      }
    });
  }

  ngAfterViewInit() {
    $('.nav-item').on('click', {
      _installationService: this._installationService,
      installations: this.installations,
      tabClick: this.tabClick
    }, this.tabsManager);
  }

  tabsManager(objs) {
    const text = $(this).text();
    if (text === 'List') {
      objs.data.tabClick.emit('List');
    } else if (text === 'Map') {
      objs.data.tabClick.emit('Map');
    }
  }

  createInstallation() {
    this.showCreateOrEditInstallationDialog();
  }

  editInstallation(installation: InstallationsDto) {
    this.showCreateOrEditInstallationDialog(installation.id);
  }

  showCreateOrEditInstallationDialog(id?: number): void {
    let createOrEditInstallationDialog: BsModalRef;
    if (!id) {
      createOrEditInstallationDialog = this._modalService.show(
        CreateInstallationComponent,
        {
          class: 'modal-lg',
        }
      );
    } else {
      createOrEditInstallationDialog = this._modalService.show(
        EditInstallationComponent,
        {
          class: 'modal-lg',
          initialState: {
            id: id,
          },
        }
      );
    }
    createOrEditInstallationDialog.content.onSave.subscribe(() => {
      this.refresh();
    });
  }

  listCityChange() {
    this.refresh();
  }

  mapCityChange() {
    this.installationsForCity();
  }

  getIcon(installation: InstallationsDto) {
    if (installation.status === 1) {
      return '../../assets/img/green-dot.svg';
    } else {
      return '../../assets/img/red-dot.svg';
    }
  }

  viewInMap(installation: InstallationsDto) {
    this._modalService.show(
        ViewInMapComponent,
        {
          class: 'modal-lg',
          initialState: {
            installation: installation,
          },
        }
      );
  }

  changeStatus(installation: InstallationsDto, status: number) {
    this._installationService.changeStatus(installation.id, status).subscribe(result => {
      this.refresh();
    });
  }

  protected list(request: PagedInstallationInstallationDto, pageNumber: number, finishedCallback: Function): void {
    request.keyword = this.keyword;
    this._installationService
      .getPagedInstallations(request.keyword, this.selectedCity, request.skipCount, request.maxResultCount)
      .pipe(
        finalize(() => {
          finishedCallback();
        })
      )
      .subscribe((result: InstallationsDtoPagedResultDto) => {
        this.installations = result.items;
        this.showPaging(result, pageNumber);
      });
  }
  protected delete(installation: InstallationsDto): void {
    abp.message.confirm(
      this.l('InstallationDeleteWarningMessage', installation.serial),
      undefined,
      (result: boolean) => {
        if (result) {
          this._installationService
            .delete(installation.id)
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
}
