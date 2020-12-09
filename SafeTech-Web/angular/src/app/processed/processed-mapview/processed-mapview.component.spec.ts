import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { ProcessedMapviewComponent } from './processed-mapview.component';

describe('ProcessedMapviewComponent', () => {
  let component: ProcessedMapviewComponent;
  let fixture: ComponentFixture<ProcessedMapviewComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ ProcessedMapviewComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(ProcessedMapviewComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
