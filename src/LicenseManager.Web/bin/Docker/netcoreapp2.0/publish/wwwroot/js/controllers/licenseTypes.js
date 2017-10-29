//Controller for Licenses' Index View to get all license types names
angular.module('app').controller('GetLicenseTypeNameCtrl',['$scope', '$http','licenseTypesFactory', function ($scope, $http, licenseTypesFactory) {
    licenseTypesFactory.GetLicenseType($scope.license.licenseTypeId)
        .then(function success(response){
            $scope.licenseTypeName = response.data.name;
        });
}]);
//Controller for LicenseTypes' Index View
angular.module('app').controller('BrowseLicenseTypesCtrl',['$scope', '$http', '$ngConfirm', '$location','licenseTypesFactory', function ($scope, $http, $ngConfirm, $location, licenseTypesFactory) {
    licenseTypesFactory.BrowseLicenseTypes()
        .then(function success(response){
            $scope.licenseTypes = response.data;
        }), function error(response){
            $window.alert(response.error);
        }

    $scope.DeleteButton = function(licenseTypeId){
        $ngConfirm({
            title: 'Please confirm',
            content: 'Are you sure you want to delete license Type?',
            scope: $scope,
            buttons: {
                YesButton: {
                    text: 'YES',
                    btnClass: 'btn-blue',
                    action: function () {
                        licenseTypesFactory.DeleteLicenseType(licenseTypeId)
                            .then(function success(response){
                                location.reload();
                            }), function error(response){
                                $window.alert(response.error);
                            }
                    }
                },
                No: function (scope, button) {
                }
            }
        });
    }
}]);
//Controller for LicenseType's Add View
angular.module('app').controller('AddLicenseTypeFormCtrl', ['$scope', '$http', '$window', 'licenseTypesFactory', 'licenseTypesDisplayFactory', 
    function($scope, $http, $window, licenseTypesFactory, licenseTypesDisplayFactory){  
    $scope.AddLicenseTypeButton = function (){
        licenseTypesFactory.AddLicenseType($scope.name)
        .then(function success(response){
            licenseTypesDisplayFactory.AddDisplay();
        }), function error(response){
            $window.alert(response.error);
        }
    }
}]);