//Controller for Computer's Add View
angular.module('app').controller('BrowseRoomsCtrl', ['$scope', '$http', '$ngConfirm', '$location', 'roomsFactory', 'displayErrorFactory',
    function($scope, $http, $ngConfirm, $location, roomsFactory, displayErrorFactory){
        //Get all rooms
        roomsFactory.BrowseRooms()
            .then(function success(response){
                $scope.rooms = response.data;
            }, function error(response){
                displayErrorFactory.DisplayError('Cannot load data');
            });
        
        $scope.DeleteButton = function(roomId){
            $ngConfirm({
                title: 'Please confirm',
                content: 'Are you sure you want to delete room?',
                scope: $scope,
                buttons: {
                    YesButton: {
                        text: 'YES',
                        btnClass: 'btn-blue',
                        action: function () {
                            roomsFactory.DeleteRoom(roomId)
                                .then(function success(response){
                                    location.reload();
                                }, function error(response){
                                    if(response.status === 401)
                                    {
                                        displayErrorFactory.DisplayError(response.data.message);
                                    }
                                    else
                                    {
                                        displayErrorFactory.DisplayError("Cannot delete room");
                                    }
                                    
                                });
                        }
                    },
                    No: function (scope, button) {
                    }
                }
            });
        }
    }]);
//Controller for Room's Add View
angular.module('app').controller('AddRoomFormCtrl', ['$scope', '$http', '$window', '$location', 'roomsFactory', 'displayErrorFactory', 'roomDisplayFactory',
    function($scope, $http, $window, $location, roomsFactory, displayErrorFactory, roomDisplayFactory){

        $scope.AddRoomButton = function(){
            roomsFactory.AddRoom($scope.name)
                .then(function success(response){
                    roomDisplayFactory.AddDisplay();
                }, function errorCallback(response){
                    if(response.status === 401)
                    {
                        displayErrorFactory.DisplayError(response.data.message);
                    }
                    else
                    {
                        displayErrorFactory.DisplayError('Cannot add room');
                    }
                    
                });
        }
}]);

angular.module('app').controller('GetRoomNameCtrl', ['$scope', '$http', 'roomsFactory', 'displayErrorFactory', 
    function ($scope, $http, roomsFactory, displayErrorFactory) {
    roomsFactory.GetRoom($scope.computer.roomId)
        .then(function successCallback(response) {
            $scope.roomName = response.data.name;
        }, function errorCallback(response) {
            if(response.status === 401){
                displayErrorFactory.DisplayError(response.data.message);
            }
        });
}]);