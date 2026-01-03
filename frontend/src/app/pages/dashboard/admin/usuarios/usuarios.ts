import { Component } from '@angular/core';

import { UserData } from '../../../../shared/components/admin-components/user/user-data/user-data';
import { UserFinder } from '../../../../shared/components/admin-components/user/user-finder/user-finder';

@Component({
  selector: 'app-usuarios',
  standalone: true,
  imports: [UserData, UserFinder],
  templateUrl: './usuarios.html',
  styleUrls: ['./usuarios.css'],
})
export class Usuarios {

}
