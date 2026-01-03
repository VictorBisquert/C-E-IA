import { Component } from '@angular/core';

import { InstallationData } from '../../../../shared/components/admin-components/installation/installation-data/installation-data';
import { InstallationFinder } from '../../../../shared/components/admin-components/installation/installation-finder/installation-finder';

@Component({
  selector: 'app-installations',
  standalone: true,
  imports: [InstallationData, InstallationFinder],
  templateUrl: './installations.html',
  styleUrls: ['./installations.css'],
})
export class Installations {

}
