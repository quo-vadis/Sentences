import { Component, Inject, Input } from '@angular/core';
import { Observable } from 'rxjs/Observable';
import { FormGroup, FormControl, Validators, NgForm } from '@angular/forms';
import { Headers } from '@angular/http/src/headers';
import { HttpClient } from '@angular/common/http';
import { ToastrService } from 'ngx-toastr';
import { ISentence } from '../Interfaces/ISentence';
import { FetchSentencesComponent } from '../fetchSentences/fetchSentences.component';


@Component({
    selector: 'app',
    templateUrl: './app.component.html',
    styleUrls: ['./app.component.css']
})
export class AppComponent{
    uploadForm: FormGroup;
    query: FormControl;
    baseURL: string = "";
    fileToUpload: any;
    length: number;

    sentences: ISentence[];

    constructor(private toastr: ToastrService,
        private http: HttpClient,
        @Inject('BASE_URL') private baseUrl: string) {

        http.get(baseUrl + 'api/Sentence').subscribe(
            result => {
                this.sentences = (result as ISentence[]).reverse();
            },
            error => this.toastr.error(error)
        );

        this.query = new FormControl('', Validators.required);
        this.uploadForm = new FormGroup({
            query: this.query
        });
    }

    handleFileInput(files: FileList) {
        let fileArray = Array.from(files);
        if (files.item(0) == null || !files.item(0).name.endsWith('.txt')) {
            this.toastr.error("Please select file. Support only text files with .txt extension");
            this.fileToUpload = null;
            fileArray = [];
        } else {           
            this.fileToUpload = files.item(0);
        }
    }

    handleSubmit(event: any, uploadNgForm: NgForm, uploadForm: FormGroup) {
        event.preventDefault();
        if (uploadNgForm.submitted) {
           this.fileUpload(this.fileToUpload);
        }
    }   

    fileUpload(fileItem: File): void {

        let apiUpload = this.baseUrl + 'api/Upload';
        const formData: FormData = new FormData();

        formData.append('file', fileItem);
        formData.append('query', this.query.value);

        this.http.post(apiUpload, formData).subscribe(
            res => {
                if (res as ISentence[] != null) {
                    this.sentences = (res as ISentence[]).reverse();
                    this.toastr.success(`Data successfully added`);
                }
                else {
                    this.toastr.info("Matches not found");
                }
               
            },
            error => {
                this.toastr.error(`Can't upload file`);
            });
    }
}