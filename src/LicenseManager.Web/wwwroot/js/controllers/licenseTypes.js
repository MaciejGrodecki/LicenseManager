angular.module('app').controller('GetLicenseTypeNameCtrl',['$scope', '$http','licenseTypesFactory', function ($scope, $http, licenseTypesFactory) {
    licenseTypesFactory.GetLicenseType($scope.license.licenseTypeId)
        .then(function success(response){
            $scope.licenseTypeName = response.data.name;
        });
}]);

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

angular.module('app').controller('AddLicenseTypeFormCtrl', ['$scope', '$http', '$window', 'licenseTypesFactory', function($scope, $http, $window, licenseTypesFactory){
    
    $scope.AddLicenseTypeButton = function (){
        licenseTypesFactory.AddLicenseType($scope.name)
        .then(function success(response){
            $window.alert('Added license type');
            $window.location.href = 'http://localhost:5050/licenseTypes/index';
        }), function error(response){
            $window.alert(response.error);
        }
    }
}]);