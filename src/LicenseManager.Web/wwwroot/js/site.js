function AddLicenseRedirect() {
    location.href = "http://localhost:5050/license/Add/ ";
}

function MainPageRedirect() {
    location.href = "http://localhost:5050/ ";
}

function AddRoomRedirect() {
    location.href = "http://localhost:5050/room/Add/";
}

function AddLicenseTypeRedirect() {
    location.href = "http://localhost:5050/licenseType/Add";
}

function BrowseLicenseTypesRedirect() {
    location.href = "http://localhost:5050/licenseTypes/index";
}

function AddComputerRedirect() {
    location.href = "http://localhost:5050/computer/add";
}

function BrowseComputerRedirect() {
    location.href = "http://localhost:5050/computers/index";
}

var app = angular.module('app', ['cp.ngConfirm']);

app.controller("BrowseLicensesCtrl", function ($scope, $http) {
    $http({
        method: 'GET',
        url: 'http://localhost:5000/licenses/'
    }).then(function successCallback(response) {
        $scope.licenses = response.data;
    }), function errorCallback(response) {
    };

    $scope.selectLicense = function (licenseId) {
        location.href = "http://localhost:5050/license/" + licenseId;
    };
});

app.controller("GetLicenseTypeNameCtrl", function ($scope, $http) {
    $http({
        method: 'GET',
        url: "http://localhost:5000/licenseTypes/" + $scope.license.licenseTypeId
    }).then(function successCallback(response) {
        $scope.licenseTypeName = response.data.name;
    }), function errorCallback(response) {
    };
});

app.controller("AddLicenseFormCtrl", function ($scope, $http, $window) {
    $http({
        method: 'GET',
        url: "http://localhost:5000/licenseTypes/"
    }).then(function successCallback(response) {
        $scope.licenseTypes = response.data;
    }), function errorCallback(response) {
    };

    $scope.AddLicenseButton = function () {
        $http({
            method: 'POST',
            url: "http://localhost:5000/licenses",
            dataType: 'application/json',
            data: {
                name: $scope.name,
                count: $scope.count,
                licenseTypeId: $scope.ddlLicenseTypes,
                buyDate: $scope.buyDate
            }
        }).then(function successCallback(response) {
            $window.alert('Added license');
            $window.location.href = 'http://localhost:5050/';
        });
    };
});

app.controller("DetailsLicenseFormCtrl", function ($scope, $http, $location, $window, $filter,
    $ngConfirm) {
    $scope.isDisabled = true;
    var currentLicenseId = $location.absUrl().split('/')[4];
    var currentLicenseTypeId;
    $http({
        method: 'GET',
        url: "http://localhost:5000/licenses/" + currentLicenseId
    }).then(function successCallback(response) {
        $scope.license = response.data;
        var json = {
            initDate: $scope.license.buyDate
        };
        var formatedBuyDate = new Date(json.initDate);
        $scope.license.buyDate = $filter('date')(formatedBuyDate, 'yyyy-MM-dd');
        $http({
            method: 'GET',
            url: "http://localhost:5000/licenseTypes/" + $scope.license.licenseTypeId
        }).then(function successCallback(response) {
            $scope.licenseTypes = response.data;
            var arrayOfLicenseTypes = [];
            for (var key in $scope.licenseTypes) {
                var tmp = {};
                tmp[key] = $scope.licenseTypes[key];
                arrayOfLicenseTypes.push(tmp);
            }
            $scope.licenseTypes = arrayOfLicenseTypes;
        });
    });

    $scope.DeleteButton = function () {
        $ngConfirm({
            title: 'Please confirm',
            content: 'Are you sure you want to delete <b>' + $scope.license.name + '</b> license?',
            scope: $scope,
            buttons: {
                YesButton: {
                    text: 'YES',
                    btnClass: 'btn-blue',
                    action: function () {
                        $http({
                            method: 'DELETE',
                            url: "http://localhost:5000/licenses/" + currentLicenseId
                        }).then(function successCallback(response) {
                            $window.location.href = 'http://localhost:5050/';
                        });
                    }
                },
                No: function (scope, button) {
                }
            }
        });
    };

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
                        $http({
                            method: 'PUT',
                            url: "http://localhost:5000/licenses/" + currentLicenseId,
                            dataType: 'application/json',
                            data: {
                                name: $scope.license.name,
                                count: $scope.license.count,
                                licenseTypeId: $scope.ddlLicenseTypes,
                                buyDate: $scope.license.buyDate
                            }
                        }).then(function successCallback(response) {
                            $window.location.href = 'http://localhost:5050/';
                        });
                    }
                },
                No: function (scope, button) {
                }
            }
        });
    };

    $scope.UnlockForm = function () {
        $scope.isDisabled = false;
        var licenseType = $scope.ddlLicenseTypes;

        $http({
            method: 'GET',
            url: "http://localhost:5000/licenseTypes/"
        }).then(function successCallback(response) {
            $scope.licenseTypes = response.data;
            $scope.ddlLicenseTypes = $scope.license.licenseTypeId;
        });
    };
});

app.controller("BrowseRoomsCtrl", function ($scope, $http, $ngConfirm, $location) {
    $http({
        method: 'GET',
        url: 'http://localhost:5000/rooms'
    }).then(function successCallback(response) {
        $scope.rooms = response.data;
    });

    $scope.DeleteButton = function (roomId) {
        $ngConfirm({
            title: 'Please confirm',
            content: 'Are you sure you want to delete room?',
            scope: $scope,
            buttons: {
                YesButton: {
                    text: 'YES',
                    btnClass: 'btn-blue',
                    action: function () {
                        $http({
                            method: 'DELETE',
                            url: "http://localhost:5000/rooms/" + roomId
                        }).then(function successCallback(response) {
                            location.reload();
                        })
                    }
                },
                No: function (scope, button) {
                }
            }
        });
    }
});

app.controller("AddRoomFormCtrl", function ($scope, $http, $window) {
    $scope.AddRoomButton = function () {
        $http({
            method: 'POST',
            url: "http://localhost:5000/rooms",
            dataType: 'application/json',
            data: {
                name: $scope.name
            }
        }).then(function successCallback(response) {
            $window.alert('Added room');
            $window.location.href = 'http://localhost:5050/rooms/index';
        });
    }
});

app.controller("BrowseLicenseTypesCtrl", function ($scope, $http, $ngConfirm, $location) {
    $http({
        method: 'GET',
        url: 'http://localhost:5000/licenseTypes'
    }).then(function successCallback(response) {
        $scope.licenseTypes = response.data;
    });

    $scope.DeleteButton = function (licenseTypeId) {
        $ngConfirm({
            title: 'Please confirm',
            content: 'Are you sure you want to delete license Type?',
            scope: $scope,
            buttons: {
                YesButton: {
                    text: 'YES',
                    btnClass: 'btn-blue',
                    action: function () {
                        $http({
                            method: 'DELETE',
                            url: "http://localhost:5000/licenseTypes/" + licenseTypeId
                        }).then(function successCallback(response) {
                            location.reload();
                        })
                    }
                },
                No: function (scope, button) {
                }
            }
        });
    }
});

app.controller("AddLicenseTypeFormCtrl", function ($scope, $http, $window) {
    $scope.AddLicenseTypeButton = function () {
        $http({
            method: 'POST',
            url: "http://localhost:5000/licenseTypes",
            dataType: 'application/json',
            data: {
                name: $scope.name
            }
        }).then(function successCallback(response) {
            $window.alert('Added license type');
            $window.location.href = 'http://localhost:5050/licenseTypes/index';
        });
    }
});

app.controller("BrowseComputersCtrl", function ($scope, $http) {
    $http({
        method: 'GET',
        url: 'http://localhost:5000/computers/'
    }).then(function successCallback(response) {
        $scope.computers = response.data;
    }), function errorCallback(response) {
    };

    $scope.selectComputer = function (computerId) {
        location.href = "http://localhost:5050/computer/" + computerId;
    };
});

app.controller("GetRoomNameCtrl", function ($scope, $http) {
    $http({
        method: 'GET',
        url: "http://localhost:5000/rooms/" + $scope.computer.roomId
    }).then(function successCallback(response) {
        $scope.roomName = response.data.name;
    }), function errorCallback(response) {
    };
});

app.controller("AddComputerFormCtrl", function ($scope, $http, $window) {
    $http({
        method: 'GET',
        url: "http://localhost:5000/rooms/"
    }).then(function successCallback(response) {
        $scope.rooms = response.data;
    }), function errorCallback(response) {
        };

    $http({
        method: 'GET',
        url: "http://localhost:5000/users/"
    }).then(function successCallback(response) {
        $scope.users = response.data;
    }), function errorCallback(response) {
    };

    $scope.AddComputerButton = function () {
        $http({
            method: 'POST',
            url: "http://localhost:5000/computers",
            dataType: 'application/json',
            data: {
                inventoryNumber: $scope.inventoryNumber,
                ipAddress: $scope.ipAddress,
                roomId: $scope.ddlRooms,
                usersId: $scope.ddlUsers
            }
        }).then(function successCallback(response) {
            console.log($scope.ddlUsers);
            $window.alert('Added computer');
            $window.location.href = 'http://localhost:5050/computers/index';
        });
    };
});

app.controller("DetailsComputerFormCtrl", function ($scope, $http, $location, $window, $filter,
    $ngConfirm) {
    $scope.isDisabled = true;
    var currentComputerId = $location.absUrl().split('/')[4];
    var currentRoomId;
    $http({
        method: 'GET',
        url: "http://localhost:5000/computers/" + currentComputerId
    }).then(function successCallback(response) {
        $scope.computer = response.data;
        $http({
            method: 'GET',
            url: "http://localhost:5000/rooms/" + $scope.computer.roomId
        }).then(function successCallback(response) {
            $scope.rooms = response.data;
            var arrayOfRooms = [];
            for (var key in $scope.rooms) {
                var tmp = {};
                tmp[key] = $scope.rooms[key];
                arrayOfRooms.push(tmp);
            }
            $scope.rooms = arrayOfRooms;
        });
    });

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
                        $http({
                            method: 'DELETE',
                            url: "http://localhost:5000/computers/" + currentComputerId
                        }).then(function successCallback(response) {
                            $window.location.href = 'http://localhost:5050/computers/index';
                        });
                    }
                },
                No: function (scope, button) {
                }
            }
        });
    };

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
                        $http({
                            method: 'PUT',
                            url: "http://localhost:5000/computers/" + currentComputerId,
                            dataType: 'application/json',
                            data: {
                                inventoryNumber: $scope.computer.inventoryNumber,
                                ipAddress: $scope.computer.ipAddress,
                                roomId: $scope.ddlRooms,
                                usersId: $scope.ddlUsers
                            }
                        }).then(function successCallback(response) {
                            
                        });
                    }
                },
                No: function (scope, button) {
                }
            }
        });
    };

    $scope.UnlockForm = function () {
        $scope.ddlUsers = $scope.computer.users.map(a => a.userId);
        $scope.isDisabled = false;
        var licenseType = $scope.ddlRooms;
        $http({
            method: 'GET',
            url: 'http://localhost:5000/users/'
        }).then(function successCallback(response) {
            $scope.computer.users = response.data;
            
        })
        
    

        $http({
            method: 'GET',
            url: "http://localhost:5000/rooms/"
        }).then(function successCallback(response) {
            $scope.rooms = response.data;
            $scope.ddlRooms = $scope.computer.roomId;
            });
    };
});