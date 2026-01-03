import { Component } from '@angular/core';

import { ScaleData } from '../../../../shared/components/admin-components/scale/scale-data/scale-data';
import { ScaleFinder } from '../../../../shared/components/admin-components/scale/scale-finder/scale-finder';

@Component({
  selector: 'app-basculas',
  standalone: true,
  imports: [ScaleData, ScaleFinder],
  templateUrl: './basculas.html',
  styleUrls: ['./basculas.css'],
})
export class Basculas {

}
