import { Component, Inject, Input, OnChanges, SimpleChanges, SimpleChange } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { ISentence } from '../Interfaces/ISentence';
import { ToastrService } from 'ngx-toastr';
@Component({
    selector: 'sentences',
    templateUrl: './fetchSentences.component.html'
})
export class FetchSentencesComponent implements OnChanges{

    @Input() fetchedSentences: ISentence[]= [];

    ngOnChanges(changes: SimpleChanges): void {
        const fetchedSentences: SimpleChange = changes.fetchedSentences;
        this.fetchedSentences = fetchedSentences.currentValue as ISentence[];
    }

    constructor(http: HttpClient,
                @Inject('BASE_URL') baseUrl: string,
        private toastr: ToastrService) {

            http.get(baseUrl + 'api/Sentence').subscribe(
                result => {
                    this.fetchedSentences = result as ISentence[];
                },
                error => this.toastr.error(error)
            ); 
    }

}
