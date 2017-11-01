//Controller for Computer's Add View
angular.module('app').controller('BrowseRoomsCtrl', ['$scope', '$http', '$ngConfirm', '$location', 'roomsFactory',
    function($scope, $http, $ngConfirm, $location, roomsFactory){
        //Get all rooms
        roomsFactory.BrowseRooms()
            .then(function success(response){
                $scope.rooms = response.data;
            }), function error(response){
                $window.alert(response.error)
            }
        
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
//Controller for Room's Add View
angular.module('app').controller('AddRoomFormCtrl', ['$scope', '$http', '$window', '$location', 'roomsFactory', 'displayErrorFactory',
    function($scope, $http, $window, $location, roomsFactory, displayErrorFactory){

        $scope.AddRoomButton = function(){
            roomsFactory.AddRoom($scope.name)
                .then(function success(response){
                    $window.alert('Room was added');
                    $window.location.href = serverAddress + '/rooms/index';
                    console.log(response.status);
                }, function err(error){
                    if(error.status === 401)
                    {
                        displayErrorFactory.DisplayError(error.data.message);
                    }
                    
                });
        }
}]);

angular.module('app').controller('GetRoomNameCtrl', ['$scope', '$http', 'roomsFactory', function ($scope, $http, roomsFactory) {
    roomsFactory.GetRoom($scope.computer.roomId)
        .then(function successCallback(response) {
            $scope.roomName = response.data.name;
        }), function errorCallback(response) {
        };
}]);