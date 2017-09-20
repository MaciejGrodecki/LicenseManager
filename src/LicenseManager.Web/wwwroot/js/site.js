$(document).ready(function() {
    $('select').material_select();
  });

$('.datepicker').pickadate({
    selectMonths: true, // Creates a dropdown to control month
    selectYears: 20, // Creates a dropdown of 15 years to control year,
    today: 'Dzisiaj',
    clear: 'Clear',
    close: 'Ok',
    closeOnSelect: false, // Close upon selecting a date,
    monthsFull: [ 'styczeń', 'luty', 'marzec', 'kwiecień', 'maj', 'czerwiec', 'lipiec', 'sierpień', 'wrzesień', 'październik', 'listopad', 'grudzień' ],
    monthsShort: [ 'sty', 'lut', 'mar', 'kwi', 'maj', 'cze', 'lip', 'sie', 'wrz', 'paź', 'lis', 'gru' ],
    weekdaysFull: [ 'niedziela', 'poniedziałek', 'wtorek', 'środa', 'czwartek', 'piątek', 'sobota' ],
    weekdaysShort: [ 'niedz.', 'pn.', 'wt.', 'śr.', 'cz.', 'pt.', 'sob.' ],
    today: 'Dzisiaj',
    clear: 'Usuń',
    close: 'Zamknij',
    firstDay: 1,
    format: 'dd.mm.yyyy',
    formatSubmit: 'dd.mm.yyyy'
  }); 

$(function() {
    init();
    function init() {
        ko.applyBindings(new ViewModel());
    };

    function ViewModel()
    {
        var self = this;
        self.licenses = ko.observableArray([]);
        self.license = ko.observable(new LicenseViewModel(""));
        self.licenseTypes = ko.observableArray([]);
        selectedLicenseType : ko.observable();

        self.getLicense = function(licenseId){
            $.get("http://localhost:5000/licenses/" + licenseId, function(response){
                self.license(new LicenseViewModel(response.name, response.count, response.buyDate, response.licenseTypeId));
            })
        }

        loadLicenseTypes();
        function loadLicenseTypes(){
            $.get("http://localhost:5000/licensetypes", function(response){
                self.licenseTypes(response);
            })
        }
        
        loadLicenses();
        function loadLicenses() {
            $.get("http://localhost:5000/licenses", function(response){
                self.licenses(response);
            })
        }

        function LicenseViewModel(name, count, buyDate, licenseTypeId){
            this.name = ko.observable(name);
            this.count = ko.observable(count);
            this.buyDate = ko.observable(buyDate);
            this.licenseTypeId = ko.observable(licenseTypeId);
        }
    }
})();

