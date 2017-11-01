//Controller for Computers' Index View
angular.module('app').controller('BrowseComputersCtrl',['$http', '$scope', 'computersFactory', function($http, $scope, computersFactory){
    //Get all computers
    computersFactory.BrowseComputers()
        .then(function success(response){
            $scope.computers = response.data;
        }), function error(response){
            $window.alert(response.error);
        }

    $scope.selectComputer = function(computerId){
        location.href = serverAddress + '/computer/' + computerId;
    }
}]);

//Controller for Computer's Add View
angular.module('app').controller('AddComputerFormCtrl',['$http', '$scope', '$window', 'computersFactory', 'roomsFactory', 'usersFactory', 'displayComputerFactory',
    function($http, $scope, $window, computersFactory, roomsFactory, usersFactory, displayComputerFactory){
        //get all rooms
        roomsFactory.BrowseRooms()
            .then(function success(response){
                $scope.rooms = response.data;
            }), function error(response){
                $window.alert(response.error);
            }
        //get all users
        usersFactory.BrowseUsers()
            .then(function success(response){
                $scope.users = response.data;
            }), function error(response){
                $window.alert(response.error);
            }
        
        $scope.AddComputerButton = function(){
            computersFactory.AddComputer(
                $scope.inventoryNumber,
                $scope.ipAddress,
                $scope.ddlRooms,
                $scope.ddlUsers
            ).then(function success(response){
                displayComputerFactory.AddDisplay();
            }), function error(response){
                $window.alert(response.error);
            }
        }

}]);
//Controller for Computer's Details View
angular.module('app').controller('DetailsComputerFormCtrl',['$scope', '$http', '$location', '$window', '$filter', '$ngConfirm', 'computersFactory', 
    'usersFactory', 'roomsFactory', 'displayComputerFactory', function($scope, $http, $location, $window, $filter, $ngConfirm, computersFactory, 
        usersFactory, roomsFactory, displayComputerFactory){

        $scope.isDisabled = true;
        //Get computerId from URL
        var currentComputerId = $location.absUrl().split('/')[4];
        //Get current computer data
        computersFactory.GetComputer(currentComputerId)
            .then(function success(response){
                $scope.computer = response.data;
                roomsFactory.GetRoom($scope.computer.roomId)
                    .then(function success(response){
                        $scope.rooms = [response.data];
                        $scope.ddlRooms = $scope.computer.roomId;
                    });
            }), function error(response){
                $window.alert(response.error);
            }

            $scope.DeleteButton = function () {
                $ngConfirm({
                    title: 'Please confirm',
                    content: 'Are you sure you want to delete this computer?',
                    scope: $scope,
                    buttons: {
                        YesButton: {
                            text: 'YES',
                            btnClass: 'btn-blue',
                            action: function () {
                                computersFactory.DeleteComputer(currentComputerId)
                                    .then(function success(response){
                                        displayComputerFactory.DeleteDisplay();
                                    }), function error(response){
                                        $window.alert(response.error);
                                    }
                            }
                        },
                        No: function (scope, button) {
                        }
                    }
                });
            };

            $scope.SaveButton = function(){
                $ngConfirm({
                    title: 'Please confirm',
                    content: 'Are you sure you want save changes?',
                    scope: $scope,
                    buttons: {
                        YesButton: {
                            text: 'YES',
                            btnClass: 'btn-blue',
                            action: function () {
                                computersFactory.UpdateComputer(
                                    currentComputerId,
                                    $scope.computer.inventoryNumber,
                                    $scope.computer.ipAddress,
                                    $scope.ddlRooms,
                                    $scope.ddlUsers
                                ).then(function success(response){
                                    displayComputerFactory.SaveDisplay();  
                                }), function error( sponse){
                                    $window.alert(response.error);
                                }
                            }
                        },
                        No: function (scope, button) {
                        }
                    }
                });
            }

            $scope.UnlockForm = function () {
                $scope.ddlUsers = $scope.computer.users.map(function(a) { return a.userId;});
                $scope.isDisabled = false;
                var licenseType = $scope.ddlRooms;
                
                usersFactory.BrowseUsers()
                    .then(function success(response){
                        $scope.computer.users = response.data;
                    }), function error(response){
                        $window.alert(response.error);
                    }

                roomsFactory.BrowseRooms()
                    .then(function success(response) {
                        $scope.rooms = response.data;
                        $scope.ddlRooms = $scope.computer.roomId;
                        }), function error(response){
                            $window.alert(response.error);
                        }
                };
}]);