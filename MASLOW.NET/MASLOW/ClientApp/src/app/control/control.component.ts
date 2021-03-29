import { Component, OnInit } from '@angular/core';
import { delay } from 'rxjs/operators';
import { Item, ItemService } from '../item/item.service';

@Component({
  selector: 'control',
  templateUrl: './control.component.html',
  styleUrls: ['./control.component.scss']
})
export class ControlComponent implements OnInit {


  public items: Item[] = [];
  public temp: string;

  constructor(private itemService : ItemService) {
    itemService.getItem().subscribe(result => {
      this.items = result;
      this.isDoorOpen(this.items[0]);
      this.getTemp();
    }, error => console.error(error));

  }
  
  ngOnInit(): void {
  }

  isDoorOpen(item: Item) {
    
    this.itemService.getSonsor(item.id, "IsOpen").subscribe(result => {
      item.values[0] = result;
    })
  }

  doorAction() {
    let action : string;
    switch(this.items[0].values[0]){
      case "open" : action = "Close";
      break;
      case "close" : action = "Open";
      break;
    }

    this.itemService.doAction(this.items[0].id, action).subscribe(result => {
      delay(10000);
      this.isDoorOpen(this.items[0]);
    })
  }

  getTemp() {
    this.itemService.getSonsor(this.items[1].id, "Temp").subscribe(result => {
      this.temp = result;
    })
  }
  
  

}
