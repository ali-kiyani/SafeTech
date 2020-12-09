import { ViewportScroller } from '@angular/common';
import { Component, Injector, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { appModuleAnimation } from '@shared/animations/routerTransition';
import { AppComponentBase } from '@shared/app-component-base';
import { ProcessedDetailsDto, ProcessedMetadata, ProcessedServiceProxy } from '@shared/service-proxies/service-proxies';
import { Lightbox } from 'ngx-lightbox';

@Component({
  selector: 'app-processed-mapview',
  templateUrl: './processed-mapview.component.html',
  styleUrls: ['./processed-mapview.component.css'],
  animations: [appModuleAnimation()]
})
export class ProcessedMapviewComponent extends AppComponentBase implements OnInit {

  lat = 30.0497935;
  lng = 60.3349021;
  requestId: number;
  metadata: ProcessedMetadata;
  tempMeta: ProcessedMetadata;
  previous;

  constructor(injector: Injector, private _processedService: ProcessedServiceProxy,
    private route: ActivatedRoute, private viewportScroller: ViewportScroller, private _lightbox: Lightbox) {
    super(injector);
    this.route.params.subscribe(param => {
      this.requestId = param.id;
    });
   }

  ngOnInit(): void {
    this._processedService.getProcessedMetadata(this.requestId).subscribe(metadata => {
      this.metadata = metadata;
      this.lat = metadata.request.lat;
      this.lng = metadata.request.lng;
      this.tempMeta = metadata.clone();
      this.metadata.processedDetails = [];
      let dto: ProcessedDetailsDto;
      for (let i = 0; i < this.tempMeta.processedDetails.length; i++) {{
        dto = this.tempMeta.processedDetails.find(x => x.installations.id === this.tempMeta.processedDetails[i].installations.id);
        if (this.metadata.processedDetails.findIndex(y => y.installations.id === dto.installations.id) === -1) {
          this.metadata.processedDetails.push(dto.clone());
        }
      }}
      debugger;
    });
  }

  getIcon() {
    return '../../assets/img/bullseye.svg';
  }

  getDetailIcon() {
    return '../../assets/img/black-pin.svg';
  }

  openImage(detail: ProcessedDetailsDto) {
    let tempList = this.tempMeta.processedDetails.filter(x => x.installations.id === detail.installations.id)
    const imgList = [];
    for (let i = 0; i < tempList.length; i++) {
      const album = {
        src: this.processedImageUrl + this.metadata.request.id + '/' + tempList[i].fileName
      };
      imgList.push(album);
    }
    this._lightbox.open(imgList);
  }

  clickedMarker(infowindow) {
    if (this.previous) {
        this.previous.close();
    }
    this.previous = infowindow;
 }
}
