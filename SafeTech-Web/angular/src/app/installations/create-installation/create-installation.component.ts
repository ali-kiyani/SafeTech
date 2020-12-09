import { Component, EventEmitter, Injector, OnInit, Output } from '@angular/core';
import { MapComponent } from '@app/map/map.component';
import { AppComponentBase } from '@shared/app-component-base';
import { CitiesDto, CreateInstallationsDto, InstallationsDto, InstallationsServiceProxy } from '@shared/service-proxies/service-proxies';
import { BsModalRef, BsModalService } from 'ngx-bootstrap/modal';
import { finalize } from 'rxjs/operators';

@Component({
  selector: 'app-create-installation',
  templateUrl: './create-installation.component.html',
  styleUrls: ['./create-installation.component.css']
})
export class CreateInstallationComponent extends AppComponentBase implements OnInit {

  saving = false;
  installation = new CreateInstallationsDto();
  cities: CitiesDto[] = [];
  isActive = true;
  selectedCity = new CitiesDto();

  @Output() onSave = new EventEmitter<any>();

  constructor(injector: Injector,
    private _installationService: InstallationsServiceProxy,
    public bsModalRef: BsModalRef,
    private _modalService: BsModalService) {
      super(injector);
      this.installation.cityId = 0;
      this.selectedCity.lat = 33.6845867;
      this.selectedCity.lng = 73.0304453;
      this.installation.make = 'SafeTech';
      this.installation.serial = 'ST12345';
      this.installation.status = 1;
      this.installation.cityId = 5;
      this.installation.address = 'Street 4, G-10/2';
      this.installation.lat = 33.673637775122494;
      this.installation.lng = 73.00615521516114;
    }

  ngOnInit(): void {
    this._installationService.getAllCities().subscribe(result => {
      this.cities = result.items;
    });
  }

  cityChanged(value: number) {
    this.selectedCity = this.cities.find(x => x.id === Number(value));
  }

  save() {
    this.saving = true;
    if (this.isActive === false) {
      this.installation.status = 0;
    } else {
      this.installation.status = 1;
    }
    this._installationService
      .create(this.installation)
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

  pointLocationOnMap()
  {
    let createOrEditRequestDialog: BsModalRef;
    createOrEditRequestDialog = this._modalService.show(
      MapComponent,
      {
        class: 'modal-lg',
        initialState: {
          lat: this.selectedCity.lat,
          lng: this.selectedCity.lng
        }
      }
    );
    createOrEditRequestDialog.content.onSave.subscribe((latlng) => {
      this.installation.lat = latlng.lat;
      this.installation.lng = latlng.lng;
    });
  }
}
