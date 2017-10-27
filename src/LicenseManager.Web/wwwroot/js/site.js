//Redirects

function AddLicenseRedirect() {
    location.href = 'http://localhost:5050/license/Add/ ';
}

function MainPageRedirect() {
    location.href = 'http://localhost:5050/ ';
}

function AddRoomRedirect() {
    location.href = 'http://localhost:5050/room/Add/';
}

function BrowseRoomRedirect(){
    location.href = 'http://localhost:5050/rooms/index';
}

function AddLicenseTypeRedirect() {
    location.href = 'http://localhost:5050/licenseType/Add';
}

function BrowseLicenseTypesRedirect() {
    location.href = 'http://localhost:5050/licenseTypes/index';
}

function AddComputerRedirect() {
    location.href = 'http://localhost:5050/computer/add';
}

function BrowseComputerRedirect() {
    location.href = 'http://localhost:5050/computers/index';
}

function AddUserRedirect() {
    location.href = 'http://localhost:5050/user/add';
}

function BrowseUsersRedirect() {
    location.href = 'http://localhost:5050/users/index';
}

function DisplayOnSuccess(){
    $ngConfirm({
        title: 'Please confirm',
        content: 'The computer was updated',
        scope: $scope,
        buttons: {
            YesButton: {
                text: 'Computer details',
                btnClass: 'btn-blue',
                action: function (scope, button) {
                   
                }
            },
            No: {
                text: 'List of computers',
                btnClass: 'btn-blue',
                action: function(){
                    $window.location.href = 'http://localhost:5050/computers/index';
                }
            }
        }
    });
}

//JQuery datepicker
$( function(){
    $('#datepicker').datepicker({
        dateFormat: 'yy-mm-dd'
    });
});

//Display informations - Computer
angular.module('app').factory('displayComputerFactory', function($ngConfirm, $window){
    return {
        SaveDisplay : function(){
            return $ngConfirm({
                title: '<b>Computer data has been updated</b>',
                content: '',
                buttons: {
                    YesButton: {
                        text: 'Back to details',
                        btnClass: 'btn-blue',
                        action: function (scope, button) {
                            location.reload();
                        }
                    },
                    No: {
                        text: 'List of computers',
                        btnClass: 'btn-warning',
                        action: function(){
                            $window.location.href = 'http://localhost:5050/computers/index';
                        }
                    }
                }
            });
        },
        AddDisplay: function(){
            return $ngConfirm({
                title: '<b>Computer has been added</b>',
                content: '',
                autoClose: 'CloseButton|2000',
                buttons: {
                    CloseButton: {
                        text: 'Close',
                        btnClass: 'btn-info',
                        action: function(){
                            $window.location.href = 'http://localhost:5050/computers/index';
                        }
                    }
                }
            });
        },
        DeleteDisplay: function(){
            return $ngConfirm({
                title: '<b>Computer has been deleted</b>',
                content: '',
                autoClose: 'CloseButton|2000',
                buttons: {
                    CloseButton: {
                        text: 'Close',
                        btnClass: 'btn-info',
                        action: function(){
                            $window.location.href = 'http://localhost:5050/computers/index';
                        }
                    }
                }
            });
        }
    }
});

//Display informations - Licenses
angular.module('app').factory('displayLicensesFactory', function($ngConfirm, $window){
    return{
        SaveDisplay : function(){
            return $ngConfirm({
                title: '<b>License data has been updated</b>',
                content: '',
                buttons: {
                    YesButton: {
                        text: 'Back to details',
                        btnClass: 'btn-blue',
                        action: function (scope, button) {
                            location.reload();
                        }
                    },
                    No: {
                        text: 'List of licenses',
                        btnClass: 'btn-warning',
                        action: function(){
                            $window.location.href = 'http://localhost:5050/licenses/index';
                        }
                    }
                }
            });
        },
        DeleteDisplay: function(){
            return $ngConfirm({
                title: '<b>License has been deleted</b>',
                content: '',
                autoClose: 'CloseButton|2000',
                buttons: {
                    CloseButton: {
                        text: 'Close',
                        btnClass: 'btn-info',
                        action: function(){
                            $window.location.href = 'http://localhost:5050/licenses/index';
                        }
                    }
                }
            });
        },
        AddDisplay: function(){
            return $ngConfirm({
                title: '<b>License has been added</b>',
                content: '',
                autoClose: 'CloseButton|2000',
                buttons: {
                    CloseButton: {
                        text: 'Close',
                        btnClass: 'btn-info',
                        action: function(){
                            $window.location.href = 'http://localhost:5050/licenses/index';
                        }
                    }
                }
            });
        },
    }
});

//Display informations - License types
angular.module('app').factory('licenseTypesDisplayFactory', function ($ngConfirm, $window){
    return {
        AddDisplay: function(){
            return $ngConfirm({
                title: '<b>License type has been added</b>',
                content: '',
                autoClose: 'CloseButton|2000',
                buttons: {
                    CloseButton: {
                        text: 'Close',
                        btnClass: 'btn-info',
                        action: function(){
                            $window.location.href = 'http://localhost:5050/licenseTypes/index';
                        }
                    }
                }
            });
        }
    }
});

//Display informations - Users
angular.module('app').factory('usersDisplayFactory', function ($ngConfirm, $window){
    return {
        AddDisplay: function(){
            return $ngConfirm({
                title: '<b>User has been added</b>',
                content: '',
                autoClose: 'CloseButton|2000',
                buttons: {
                    CloseButton: {
                        text: 'Close',
                        btnClass: 'btn-info',
                        action: function(){
                            $window.location.href = 'http://localhost:5050/users/index';
                        }
                    }
                }
            });
        },
        SaveDisplay : function(){
            return $ngConfirm({
                title: '<b>User data has been updated</b>',
                content: '',
                buttons: {
                    YesButton: {
                        text: 'Back to details',
                        btnClass: 'btn-blue',
                        action: function (scope, button) {
                            location.reload();
                        }
                    },
                    No: {
                        text: 'List of users',
                        btnClass: 'btn-warning',
                        action: function(){
                            $window.location.href = 'http://localhost:5050/users/index';
                        }
                    }
                }
            });
        },
        DeleteDisplay: function(){
            return $ngConfirm({
                title: '<b>User has been deleted</b>',
                content: '',
                autoClose: 'CloseButton|2000',
                buttons: {
                    CloseButton: {
                        text: 'Close',
                        btnClass: 'btn-info',
                        action: function(){
                            $window.location.href = 'http://localhost:5050/users/index';
                        }
                    }
                }
            });
        }
    }
});

//License's HTTP requests
angular.module('app').factory('licensesFactory', function($http){
    return {
        BrowseLicenses: function(){
            return $http({
                method: 'GET',
                url: 'http://localhost:5000/licenses/'
            })
        },
        GetLicense: function(licenseId){
            return $http({
                method: 'GET',
                url: 'http://localhost:5000/licenses/' + licenseId
            })
        },
        AddLicense: function(name, count, licenseTypeId, buyDate, serialNumber){
            return $http({
                method: 'POST',
                url: 'http://localhost:5000/licenses',
                dataType: 'application/json',
                data: {
                    name: name,
                    count: count,
                    licenseTypeId: licenseTypeId,
                    buyDate: buyDate,
                    serialNumber: serialNumber
                }
            })
        },
        DeleteLicense: function(licenseId){
            return $http({
                method: 'DELETE',
                url: 'http://localhost:5000/licenses/' + licenseId
            })
        },
        UpdateLicense: function(licenseId, name, count, licenseTypeId, buyDate, serialNumber, computers){
            return $http({
                method: 'PUT',
                url: 'http://localhost:5000/licenses/' + licenseId,
                dataType: 'application/json',
                data: {
                    name: name,
                    count: count,
                    licenseTypeId: licenseTypeId,
                    buyDate: buyDate,
                    serialNumber: serialNumber,
                    computers: computers
                }
            })
        }
    };
});



//LicenseType's HTTP requests

angular.module('app').factory('licenseTypesFactory', function($http){
    return{
        BrowseLicenseTypes: function(){
            return $http({
                method: 'GET',
                url: 'http://localhost:5000/licenseTypes/'
            });
        },
        GetLicenseType: function(licenseTypeId){
            return $http({
                method: 'GET',
                url: 'http://localhost:5000/licenseTypes/' + licenseTypeId
            });
        },
        AddLicenseType: function(name){
            return $http({
                method: 'POST',
                url: 'http://localhost:5000/licenseTypes',
                dataType: 'application/json',
                data: {
                    name: name
                }
            });
        },
        DeleteLicenseType: function(licenseTypeId){
            return $http({
                method: 'DELETE',
                url: 'http://localhost:5000/licenseTypes/' + licenseTypeId
            });
        }
    };
});

//Computer's HTTP requests

angular.module('app').factory('computersFactory', function($http){
    return{
        BrowseComputers: function(){
            return $http({
                method: 'GET',
                url: 'http://localhost:5000/computers/'
            });
        },
        GetComputer: function(computerId){
            return $http({
                method: 'GET',
                url: 'http://localhost:5000/computers/' + computerId
            });
        },
        AddComputer: function(inventoryNumber, ipAddress, roomId, usersId){
            return $http({
                method: 'POST',
                url: "http://localhost:5000/computers/",
                dataType: 'application/json',
                data: {
                    inventoryNumber: inventoryNumber,
                    ipAddress: ipAddress,
                    roomId: roomId,
                    usersId: usersId
                }
            })
        },
        UpdateComputer: function(computerId,inventoryNumber, ipAddress, roomId, usersId){
            return $http({
                method: 'PUT',
                url: "http://localhost:5000/computers/" + computerId,
                dataType: 'application/json',
                data: {
                    inventoryNumber: inventoryNumber,
                    ipAddress: ipAddress,
                    roomId: roomId,
                    usersId: usersId
                }
            })
        },
        DeleteComputer: function(computerId){
            return $http({
                method: 'DELETE',
                url: "http://localhost:5000/computers/" + computerId
            });
        }
    };
});

//Room's HTTP requests

angular.module('app').factory('roomsFactory', function($http){
    return{
        BrowseRooms: function(){
            return $http({
                method: 'GET',
                url: 'http://localhost:5000/rooms'
            });
        },
        GetRoom:function(roomId){
            return $http({
                method: 'GET',
                url: 'http://localhost:5000/rooms/' + roomId
            });
        },
        AddRoom: function(name){
            return $http({
                method: 'POST',
                url: 'http://localhost:5000/rooms',
                dataType: 'application/json',
                data: {
                    name: name
                }
            });
        },
        DeleteRoom: function(roomId){
            return $http({
                method: 'DELETE',
                url: 'http://localhost:5000/rooms/' + roomId
            });
        }
    }
});

//User's HTTP requests

angular.module('app').factory('usersFactory', function($http){
    return{
        BrowseUsers: function(){
            return $http({
                method: 'GET',
                url: 'http://localhost:5000/users/'
            });
        },
        GetUser: function(userId){
            return $http({
                method: 'GET',
                url: 'http://localhost:5000/users/' + userId
            });
        },
        AddUser: function(name,surname){
            return $http({
                method: 'POST',
                url: "http://localhost:5000/users",
                dataType: 'application/json',
                data: {
                    name: name,
                    surname: surname
                }
            });
        },
        UpdateUser: function(userId, name, surname){
            return $http({
                method: 'PUT',
                url: "http://localhost:5000/users/" + userId,
                dataType: 'application/json',
                data: {
                    name: name,
                    surname: surname
                }
            });
        },
        DeleteUser: function(userId){
            return $http({
                method: 'DELETE',
                url: "http://localhost:5000/users/" + userId
            });
        }
    }
});