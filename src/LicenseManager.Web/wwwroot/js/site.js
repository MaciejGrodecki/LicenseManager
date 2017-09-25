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
    format: 'mm/dd/yyyy',
    formatSubmit: 'mm/dd/yyyy'
  });

  var licenseApp = angular.module("licenseApp", ['angularMaterializeDatePicker']);

  licenseApp.controller("AddLicenseController", function($scope, $http, $window){
    $http.get("http://localhost:5000/licenseTypes/")
    .then(function(response){
        $scope.licenseTypes = response.data;
        $('select').material_select();
    });

      $scope.SaveButton = function(){
          var buyDateStr = document.getElementById("buyDate").value;
          var postRequest = $http({
              method: "POST",
              url: "http://localhost:5000/licenses",
              dataType: 'application/json',
              data : { name: $scope.name, count: $scope.count, buyDate: buyDateStr, licenseTypeId: $scope.licenseTypeId},
              headers: { "Content-Type": "application/json"}
          });
          $window.location.href = 'http://localhost:5050/';
      }
  });

  licenseApp.controller("BrowseLicensesController", function($scope, $http, $filter){
      $http.get("http://localhost:5000/licenses/")
      .then(function(reponse){
          $scope.licenses = reponse.data;
      }); 
  });



  licenseApp.controller("GetLicenseType", function($scope, $http){
      $http({
          method: "GET",
          url: "http://localhost:5000/licenseTypes/" + $scope.license.licenseTypeId
      }).then(function(response){
          $scope.licenseType = response.data.name;
      })
  });
  
  var evt = document.createEvent("HTMLEvents");
  evt.initEvent("change", false, true);
