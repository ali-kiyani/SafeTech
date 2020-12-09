import { AfterViewInit, Component, EventEmitter, Injector, OnInit, Output } from '@angular/core';
import { AppComponentBase } from '@shared/app-component-base';
import { BsModalRef } from 'ngx-bootstrap/modal';

@Component({
  selector: 'app-map',
  templateUrl: './map.component.html',
  styleUrls: ['./map.component.css']
})
export class MapComponent extends AppComponentBase implements AfterViewInit {

  lat = 30.0497935;
  lng = 60.3349021;
  displayMarker = false;
  @Output() onSave = new EventEmitter<any>();
  constructor(injector: Injector, public bsModalRef: BsModalRef) {
    super(injector);
   }

  ngAfterViewInit(): void {
  }

  mapClick(event)
  {
    this.lat = event.coords.lat;
    this.lng = event.coords.lng;
    this.displayMarker = true;
  }

  save()
  {
    this.onSave.emit({lat: this.lat, lng: this.lng});
    this.bsModalRef.hide();
  }
}
