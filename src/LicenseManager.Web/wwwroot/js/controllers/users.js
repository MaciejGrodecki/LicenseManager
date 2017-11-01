//Controller for Users' Index View
angular.module('app').controller('BrowseUsersCtrl', ['$scope', '$http', 'usersFactory', function($scope, $http, usersFactory){
    usersFactory.BrowseUsers()
        .then(function success(response){
            $scope.users = response.data;
        }), function error(response){
            $window(response.error);
        }

    $scope.selectUser = function (userId) {
        location.href = serverAddress + '/user/' + userId;
    };
}]);
//Controller for User's Add View
angular.module('app').controller('AddUserFormCtrl', ['$scope', '$http', '$window', 'usersFactory', 'usersDisplayFactory',
    function($scope, $http, $window, usersFactory, usersDisplayFactory){
        
        $scope.AddUserButton = function () {
            usersFactory.AddUser(
                $scope.name,
                $scope.surname
            ).then(function success(response){
                usersDisplayFactory.AddDisplay();
            }), function error(response){
                $window.alert(response.error);
            }
        }
}]);
//Controller for User's details View
angular.module('app').controller('DetailsUserFormCtrl', ['$scope', '$http', '$location', '$ngConfirm', '$window', 'usersFactory', 'usersDisplayFactory',
    function($scope, $http, $location, $ngConfirm, $window, usersFactory, usersDisplayFactory){
        $scope.isDisabled = true;
        var currentUserId = $location.absUrl().split('/')[4];

        usersFactory.GetUser(currentUserId)
            .then(function success(response){
                $scope.user = response.data;
            }), function error(response){
                $window.alert(response.error);
            }

        $scope.UnlockForm = function () {
            $scope.isDisabled = false;
        }

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
                            usersFactory.UpdateUser(
                                currentUserId,
                                $scope.user.name,
                                $scope.user.surname
                            ).then(function success(response){
                                usersDisplayFactory.SaveDisplay();
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

        $scope.DeleteButton = function () {
            $ngConfirm({
                title: 'Please confirm',
                content: 'Are you sure you want to delete this user?',
                scope: $scope,
                buttons: {
                    YesButton: {
                        text: 'YES',
                        btnClass: 'btn-blue',
                        action: function () {
                            usersFactory.DeleteUser(currentUserId)
                                .then(function success(response){
                                    usersDisplayFactory.DeleteDisplay();
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
}]);