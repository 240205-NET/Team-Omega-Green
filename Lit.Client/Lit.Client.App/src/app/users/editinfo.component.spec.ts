import { ComponentFixture, TestBed } from '@angular/core/testing';

import { EditInfoComponent } from './editinfo.component';

describe('EditinfoComponent', () => {
  let component: EditInfoComponent;
  let fixture: ComponentFixture<EditInfoComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [EditInfoComponent]
    });
    fixture = TestBed.createComponent(EditInfoComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
