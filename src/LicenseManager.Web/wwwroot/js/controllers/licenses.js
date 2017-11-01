//Controller for Licenses' index view
angular.module('app').controller('BrowseLicensesCtrl', ['$scope', '$http', 'licensesFactory',  function ($scope, $http, licensesFactory){
    licensesFactory.BrowseLicenses()
        .then(function success(response){
            $scope.licenses = response.data;
        }), function error(reponse){
            $window.alert(reponse.error);
        }
    
    $scope.selectLicense = function (licenseId) {
        location.href = serverAddress + '/license/' + licenseId;
    };
}]);

//Controller for Licenses' add view
angular.module('app').controller('AddLicenseFormCtrl', ['$scope', '$http', '$window', 'licenseTypesFactory', 'licensesFactory', 'displayLicensesFactory',
    function($scope, $http, $window, licenseTypesFactory, licensesFactory, displayLicensesFactory){
        //Get all license types
            licenseTypesFactory.BrowseLicenseTypes()
                .then(function success(response){
                    $scope.licenseTypes = response.data;
                }), function error(response){
                    $window.alert(response.error);
            };
        
        $scope.AddLicenseButton = function(){
            licensesFactory.AddLicense(
                $scope.name,
                $scope.count,
                $scope.ddlLicenseTypes,
                $scope.buyDate,
                $scope.serialNumber
            ).then(function success(response){
                displayLicensesFactory.AddDisplay();
            }), function error(response){
                $window.alert(response.error);
            }
        }
        

}]);
//Controller for license's details view
angular.module('app').controller('DetailsLicenseFormCtrl', ['$scope', '$http', '$location', '$window', '$filter', '$ngConfirm', 'licensesFactory', 'licenseTypesFactory', 
    'computersFactory', 'roomsFactory', 'displayLicensesFactory', function($scope, $http, $location, $window, $filter, $ngConfirm, licensesFactory, licenseTypesFactory, 
    computersFactory, roomsFactory, displayLicensesFactory){
        //$scope.isDisable = true -- disable all inputs
        //$scope.isReadonly = true -- readonly for computer input
        $scope.isDisabled = true;
        $scope.isReadonly = true;
        
        //get current licenseId from URL
        var currentLicenseId = $location.absUrl().split('/')[4];
        var ll;
        //Get selected license details
        licensesFactory.GetLicense(currentLicenseId)
            .then(function success(response){
                $scope.license = response.data;
                $scope.license.buyDate = moment(response.data.buyDate).format('YYYY-MM-DD');
                //Get licenseType 
                licenseTypesFactory.GetLicenseType($scope.license.licenseTypeId)
                    .then(function success(response){
                        $scope.licenseTypes = [response.data];
                        $scope.ddlLicenseTypes = $scope.license.licenseTypeId;
                    }), function error(response){
                        $window.alert(response.error);
                    }
            });
            //Delete button
            $scope.DeleteButton = function(){
                $ngConfirm({
                    title: 'Please confirm',
                    content: 'Are you sure you want to delete <b>' + $scope.license.name + '</b> license?',
                    scope: $scope,
                    buttons: {
                        YesButton: {
                            text: 'YES',
                            btnClass: 'btn-blue',
                            action: function () {
                                licensesFactory.DeleteLicense(currentLicenseId)
                                    .then(function successCallback(response) {
                                        displayLicensesFactory.DeleteDisplay();
                                }), function error(response){
                                    $window.alert(response.error);
                                };
                            }
                        },
                        No: function (scope, button) {
                        }
                    }
                });
            }

            //Save button
            $scope.SaveButton = function () {
                $ngConfirm({
                    title: 'Please confirm',
                    content: 'Are you sure you want save changes?',
                    scope: $scope,
                    buttons: {
                        YesButton: {
                            text: 'YES',
                            btnClass: 'btn-blue',
                            action: function () {
                                licensesFactory.UpdateLicense(
                                    currentLicenseId,
                                    $scope.license.name,
                                    $scope.license.count,
                                    $scope.ddlLicenseTypes,
                                    $scope.license.buyDate,
                                    $scope.license.serialNumber,
                                    $scope.ddlComputers
                                ).then(function success(response){
                                    displayLicensesFactory.SaveDisplay();
                                }), function error(response){
                                    $window.alert(response.error);
                                };
                            }
                        },
                        No: function (scope, button) {
                        }
                    }
                });
            };

            //Unlock button
            $scope.UnlockForm = function(){
                $scope.ddlComputers = $scope.license.computers.map(function(a) { return a.computerId;});
                $scope.isDisabled = false;
                $scope.isReadonly = false;
                licenseTypesFactory.BrowseLicenseTypes()
                    .then(function success(response){
                        $scope.licenseTypes = response.data;
                        $scope.ddlLicenseTypes = $scope.license.licenseTypeId;
                    }), function error(response){
                        $window.alert(response.error);
                    }
                //Get all computers
                computersFactory.BrowseComputers()
                    .then(function success(response){
                        $scope.license.computers = response.data;
                    }), function error(reponse){
                        $window.alert(response.error)
                    }
            }

            //Doubleclick on computer's inventoryNumber event
            $scope.Open = function(){
                computersFactory.GetComputer($scope.ddlComputers)
                    .then(function success(response){
                        roomsFactory.GetRoom(response.data.roomId)
                            .then(function success(response2){
                                $ngConfirm({
                                title: 'Computer details',
                                content: 'Inventory number: <b>' + response.data.inventoryNumber + '</b> <br> IP address: <b>' 
                                            + response.data.ipAddress + ' </b> <br> Room: <b>' 
                                            + response2.data.name + '</b>',
                                scope: $scope,
                                buttons: {
                                    DetailsButton:{
                                        text: 'Details',
                                        btnClass: 'btn-blue',
                                        action: function(){
                                            location.href = serverAddress + '/computer/' + $scope.ddlComputers;
                                        }
                                    },
                                    CloseButton:{
                                        text:'Close',
                                        btnClass: 'btn-warning',
                                        action: function(){
            
                                        }
                                    }
                                }
                                });
                            })
                        
                    }), function error(response){
                        $window.alert(response.error);
                    }
            }
}]);

