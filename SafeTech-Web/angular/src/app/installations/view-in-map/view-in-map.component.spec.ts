import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { ViewInMapComponent } from './view-in-map.component';

describe('ViewInMapComponent', () => {
  let component: ViewInMapComponent;
  let fixture: ComponentFixture<ViewInMapComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ ViewInMapComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(ViewInMapComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
