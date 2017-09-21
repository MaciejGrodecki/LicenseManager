$(function() {
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

  var licenseApp = angular.module("licenseApp", ['angularMaterializeDatePicker']);

  licenseApp.controller("AddLicenseController", function($scope, $http){
      $http.get("http://localhost:5000/licenseTypes/")
      .then(function(response){
          $scope.licenseTypes = response.data;
          $('select').material_select();
      });

      $scope.SaveButton = function(){
          var postRequest = $http({
              method: "POST",
              url: "http://localhost:5000/licenses",
              dataType: 'json',
              data : { name: $scope.name, count: $scope.count, buyDate: $scope.buyDate, licenseTypeId: $scope.licenseTypeId},
              headers: { "Content-Type": "application/json"}
          });

          postRequest.error(function (data, status){
              $window.aler(data.Message);
          })
      }
  });

  licenseApp.controller("")
  
  var evt = document.createEvent("HTMLEvents");
  evt.initEvent("change", false, true);
