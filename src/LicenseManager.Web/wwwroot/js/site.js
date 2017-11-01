// var ipServerAddress = 'http://46.101.102.100:5000/';
// var serverAddress = 'http://46.101.102.100/';

var ipServerAddress = 'http://localhost:5000/';
var serverAddress = 'http://localhost:5050';

//Redirects

function AddLicenseRedirect() {
    location.href = serverAddress + 'license/Add/ ';
}

function MainPageRedirect() {
    location.href = serverAddress;
}

function AddRoomRedirect() {
    location.href = serverAddress + '/room/Add/';
}

function BrowseRoomRedirect(){
    location.href = serverAddress + '/rooms/index';
}

function AddLicenseTypeRedirect() {
    location.href = serverAddress + '/licenseType/Add';
}

function BrowseLicenseTypesRedirect() {
    location.href = serverAddress + '/licenseTypes/index';
}

function AddComputerRedirect() {
    location.href = serverAddress + '/computer/add';
}

function BrowseComputerRedirect() {
    location.href = serverAddress + '/computers/index';
}

function AddUserRedirect() {
    location.href = serverAddress + '/user/add';
}

function BrowseUsersRedirect() {
    location.href = serverAddress + '/users/index';
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
                    $window.location.href = serverAddress + '/computers/index';
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

//Display error response
angular.module('app').factory('displayErrorFactory', function($ngConfirm){
    return {
        DisplayError : function(message){
            return $ngConfirm({
                title: 'Error',
                content: message,
                buttons:{
                    CloseButton: {
                        text: 'Close',
                        btnClass: 'btn-info',
                        action: function(){
        
                        }
                    }
                }
            });
        }
    }
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
                            $window.location.href = serverAddress + '/computers/index';
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
                            $window.location.href = serverAddress + '/computers/index';
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
                            $window.location.href = serverAddress + '/computers/index';
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
                            $window.location.href = serverAddress + '/licenses/index';
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
                            $window.location.href = serverAddress + '/licenses/index';
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
                            $window.location.href = serverAddress + '/licenses/index';
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
                            $window.location.href = serverAddress + '/licenseTypes/index';
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
                            $window.location.href = serverAddress + '/users/index';
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
                            $window.location.href = serverAddress + '/users/index';
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
                            $window.location.href = serverAddress + '/users/index';
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
                url: ipServerAddress + 'licenses/'
            })
        },
        GetLicense: function(licenseId){
            return $http({
                method: 'GET',
                url: ipServerAddress +'licenses/' + licenseId
            })
        },
        AddLicense: function(name, count, licenseTypeId, buyDate, serialNumber){
            return $http({
                method: 'POST',
                url: ipServerAddress + 'licenses',
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
                url: ipServerAddress + 'licenses/' + licenseId
            })
        },
        UpdateLicense: function(licenseId, name, count, licenseTypeId, buyDate, serialNumber, computers){
            return $http({
                method: 'PUT',
                url: ipServerAddress + 'licenses/' + licenseId,
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
                url: ipServerAddress + 'licenseTypes/'
            });
        },
        GetLicenseType: function(licenseTypeId){
            return $http({
                method: 'GET',
                url: ipServerAddress + 'licenseTypes/' + licenseTypeId
            });
        },
        AddLicenseType: function(name){
            return $http({
                method: 'POST',
                url: ipServerAddress + 'licenseTypes',
                dataType: 'application/json',
                data: {
                    name: name
                }
            });
        },
        DeleteLicenseType: function(licenseTypeId){
            return $http({
                method: 'DELETE',
                url: ipServerAddress + 'licenseTypes/' + licenseTypeId
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
                url: ipServerAddress + 'computers/'
            });
        },
        GetComputer: function(computerId){
            return $http({
                method: 'GET',
                url: ipServerAddress + 'computers/' + computerId
            });
        },
        AddComputer: function(inventoryNumber, ipAddress, roomId, usersId){
            return $http({
                method: 'POST',
                url: ipServerAddress + 'computers/',
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
                url: ipServerAddress + 'computers/' + computerId,
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
                url: ipServerAddress + 'computers/' + computerId
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
                url: ipServerAddress + 'rooms'
            });
        },
        GetRoom:function(roomId){
            return $http({
                method: 'GET',
                url: ipServerAddress + 'rooms/' + roomId
            });
        },
        AddRoom: function(name){
            return $http({
                method: 'POST',
                url: ipServerAddress + 'rooms',
                dataType: 'application/json',
                data: {
                    name: name
                }
            });
        },
        DeleteRoom: function(roomId){
            return $http({
                method: 'DELETE',
                url: ipServerAddress + 'rooms/' + roomId
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
                url: ipServerAddress + 'users/'
            });
        },
        GetUser: function(userId){
            return $http({
                method: 'GET',
                url: ipServerAddress + 'users/' + userId
            });
        },
        AddUser: function(name,surname){
            return $http({
                method: 'POST',
                url: ipServerAddress + 'users',
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
                url: ipServerAddress + 'users/' + userId,
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
                url: ipServerAddress + 'users/' + userId
            });
        }
    }
});