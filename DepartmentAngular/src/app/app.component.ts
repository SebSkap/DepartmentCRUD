import { Component } from '@angular/core';
import { DepartmentService } from 'src/app/department.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent {
  
  constructor(private departmentService:DepartmentService) { }

  departmentList:any=[];
  listedDepartment: any={
                            "id": 0,
                            "name": "0"
                        };
  newDepartmentName: string;
  displayedAdd:boolean = false;
  displayedEdit:boolean = false;

  // Runs initialy
  ngOnInit(): void 
  {
    this.listDepartments();
  }

  // Lists all departments
  listDepartments()
  {
    this.departmentService.getDepartments().subscribe(
      data=>{
        this.departmentList=data;
      })
  }

  // Lists one department based on ID parameter
  listDepartment(departmentIdToList: string)
  {
    this.departmentService.getDepartment(departmentIdToList).subscribe(
      data=>{
        this.listedDepartment = data;
      })
  }

  // Adds new department
  addDepartment()
  {
    var newDepartment = {"name": this.newDepartmentName};
    this.departmentService.postDepartment(newDepartment).subscribe(
      data=>{
        this.hideAdd();
        this.listDepartments();
      }
    )
  }

  // Updates existing department
  updateDepartment()
  {
    this.departmentService.putDepartment(this.listedDepartment).subscribe(
      data=>{
        this.hideEdit();
        this.listDepartments();
      }
    )
  }

  // Delete department
  deleteDepartment(departmentIdToDelete: string)
  {
    if (confirm("Are you sure you want to delete the record?"))
    {
      this.departmentService.deleteDepartment(departmentIdToDelete).subscribe(
        data=>{
          this.listDepartments();
        }
      )
    }
  }

  // Prepares data for update
  prepareUpdateDepartment(departmentId: string)
  {
    this.listDepartment(departmentId);
    this.displayEdit();
  }

  // Displays add input
  displayAdd()
  {
    this.displayedAdd = true;
  }

  // Hides add input
  hideAdd()
  {
    this.displayedAdd = false;
  }

  // Displays edit input
  displayEdit()
  {
    this.displayedEdit = true;
  }

  // Hides edit input
  hideEdit()
  {
    this.displayedEdit = false;
  }
}
