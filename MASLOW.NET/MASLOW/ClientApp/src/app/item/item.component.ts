import { Component } from '@angular/core';
import { Item, ItemService } from './item.service';

@Component({
  selector: 'app-item',
  templateUrl: './item.component.html',
  styleUrls: ['./item.component.scss']
})
export class ItemComponent {

  public items: Item[];

  constructor(private itemService : ItemService) {
    itemService.getItem().subscribe(result => {
      this.items = result;
    }, error => console.error(error));

  }


}
