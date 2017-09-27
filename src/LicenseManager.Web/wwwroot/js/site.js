var app = angular.module('app', []);

app.controller("BrowseLicensesCtrl", function($scope, $http){
    $http({
        method: 'GET',
        url: 'http://localhost:5000/licenses/'
    }).then(function successCallback(response){
        $scope.licenses = response.data;
    }),function errorCallback(response){

    }});

app.controller("GetLicenseTypeNameCtrl", function($scope, $http){
    $http({
        method: 'GET',
        url: "http://localhost:5000/licenseTypes/" + $scope.license.licenseTypeId 
    }).then(function successCallback(response){
        $scope.licenseTypeName = response.data.name;
    }), function errorCallback(response){

    }});

app.controller("AddLicenseFormCtrl", function($scope, $http){
    
})

app.controller("BrowseLicenseTypesCtrl", function($scope, $http){
    $http({
        method: 'GET',
        url: "http://localhost:5000/licenseTypes/"
    }).then(function successCallback(response){
        $scope.licenseTypes = response.data;
    }),function errorCallback(response){

    }});

function AddLicenseRedirect(){
    location.href = "http://localhost:5050/licenses/Add/ ";
};

function MainPageRedirect(){
    location.href = "http://localhost:5050/ ";
};
