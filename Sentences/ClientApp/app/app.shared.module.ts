import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { HttpModule } from '@angular/http';
import { RouterModule } from '@angular/router';
import { ReactiveFormsModule } from '@angular/forms';

import { AppComponent } from './components/app/app.component';
import { FetchSentencesComponent } from './components/fetchSentences/fetchSentences.component';
import { HttpClientModule } from '@angular/common/http';
import { ToastrModule } from 'ngx-toastr';

@NgModule({
    declarations: [
        AppComponent,
        FetchSentencesComponent
    ],
    imports: [
        CommonModule,
        HttpClientModule,
        FormsModule,
        ReactiveFormsModule , 
        ToastrModule.forRoot()
    ]
})
export class AppModuleShared {
}
