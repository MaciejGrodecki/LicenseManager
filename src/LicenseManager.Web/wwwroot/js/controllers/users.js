//Controller for Users' Index View
angular.module('app').controller('BrowseUsersCtrl', ['$scope', '$http', 'usersFactory', 'displayErrorFactory', function($scope, $http, usersFactory, displayErrorFactory){
    usersFactory.BrowseUsers()
        .then(function success(response){
            $scope.users = response.data;
        }, function error(response){
            displayErrorFactory.DisplayError('Cannot load users');
        });

    $scope.selectUser = function (userId) {
        location.href = serverAddress + '/user/' + userId;
    };
}]);
//Controller for User's Add View
angular.module('app').controller('AddUserFormCtrl', ['$scope', '$http', '$window', 'usersFactory', 'usersDisplayFactory', 'displayErrorFactory',
    function($scope, $http, $window, usersFactory, usersDisplayFactory, displayErrorFactory){
        
        $scope.AddUserButton = function () {
            usersFactory.AddUser(
                $scope.name,
                $scope.surname
            ).then(function success(response){
                usersDisplayFactory.AddDisplay();
            }, function error(response){
                if(response.status === 401){
                    displayErrorFactory.DisplayError(response.data.message);
                }
                else{
                    displayErrorFactory.DisplayError('Cannot add user');
                }
            });
        }
}]);
//Controller for User's details View
angular.module('app').controller('DetailsUserFormCtrl', ['$scope', '$http', '$location', '$ngConfirm', '$window', 'usersFactory', 'usersDisplayFactory', 'displayErrorFactory',
    function($scope, $http, $location, $ngConfirm, $window, usersFactory, usersDisplayFactory, displayErrorFactory){
        $scope.isDisabled = true;
        var currentUserId = $location.absUrl().split('/')[4];

        usersFactory.GetUser(currentUserId)
            .then(function success(response){
                $scope.user = response.data;
            }, function error(response){
                if(response.status === 401){
                    displayErrorFactory.DisplayError(response.data.message);
                }
                else{
                    displayErrorFactory.DisplayError('Cannot load user data');
                }
            });

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
                            }, function error(response){
                                if(response.status === 401){
                                    displayErrorFactory.DisplayError(response.data.message);
                                }
                                else{
                                    displayErrorFactory.DisplayError('Cannot change user data');
                                }
                            });
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
                                }, function error(response){
                                    if(response.status === 401){
                                        displayErrorFactory.DisplayError(response.data.message);
                                    }
                                    else{
                                        displayErrorFactory.DisplayError('Cannot delete user');
                                    }
                                });
                        }
                    },
                    No: function (scope, button) {
                    }
                }
            });
        };
}]);