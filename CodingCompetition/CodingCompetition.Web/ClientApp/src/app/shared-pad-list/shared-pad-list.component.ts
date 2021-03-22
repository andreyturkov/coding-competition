import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';

import { ApiService } from '../shared/api.service';
import { CodePad } from '../models/code-pad.model';

@Component({
  selector: 'shared-pad-list',
  templateUrl: './shared-pad-list.component.html',
  styleUrls: ['./shared-pad-list.component.css']
})
export class SharedPadListComponent implements OnInit {

  pads: string[];
  isCreating: boolean;
  code = `
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace Rextester
{
	public class Program
	{
		public static void Main(string[] args)
		{
			Console.Write("Hello World");
		}
	}
}
`;

  constructor(private http: ApiService, private router: Router) {
  }

  ngOnInit(): void {
    this.http.get<string[]>('SharedPad').subscribe(result => {
      this.pads = result;
    });
  }

  createPad(): void {
    const codePad = new CodePad(this.code);
    codePad.language = Language.CSharp;
    codePad.theme = 'eclipse';

    this.isCreating = true;
    this.http.post<any>('SharedPad', codePad).subscribe(result => {
      this.pads.push(result.id);
      this.router.navigateByUrl(`/shared-pad/${result.id}`);
      this.isCreating = false;
    });
  }

  removePad(index: number): void {
    const padId = this.pads[index];
    this.http.delete<boolean>(`SharedPad/${padId}`).subscribe(result => {
      if (result) {
        this.pads.splice(index);
      }
    });
  }
}
