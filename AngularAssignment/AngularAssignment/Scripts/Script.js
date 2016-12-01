/// <reference path="angular.min.js" />

/*creat angular module with the name of Demo and define some routes for it
*/
var app = angular.module("Demo", ["ngRoute"])
                 .config(function ($routeProvider) {
                     $routeProvider
                         // provide routes, their views and their controllers here
                        .when("/list", {
                            templateUrl: "../Templates/list.html",
                            controller: "listController"
                        })

                        .when("/create", {
                            templateUrl: "Templates/create.html",
                            controller: "createController"
                        })

                         // in order ro define the ID that have to be send to method
                         // we need to create a route for it also
                        .when("/detail/:ID", {
                            templateUrl: "Templates/detail.html",
                            controller: "detailController"
                        })

                        .when("/edit/:ID", {
                            templateUrl: "Templates/edit.html",
                            controller: "editController"
                        })

                         // it used detail controller in order to list the details of 
                         // the person who has to be deleted
                        .when("/delete/:ID", {
                            templateUrl: "Templates/confirmDel.html",
                            controller: "detailController"
                        })

                        .when("/deleteCoinfirmed/:ID", {
                            templateUrl: "Templates/list.html",
                            controller: "deleteController"
                        })

                 })
    // scope is where we define the values that we want to send to views from the controller
    // with http and get we ask for request to the method in home for asking for info that they have stored in their method
                 .controller("listController", function ($scope, $http) {
                     $http.get("Home/AjaxThatReturnsJson")
                        .then(function (response) {
                            $scope.list = response.data;
                            $scope.message ="People List"
                        })
                     
                 })

// in order ro redirect to new route after we finish in this view, we use $location 
// and later assign the destination url to it
                 .controller("createController", function ($scope, $http, $location) {
                     // we take countries by the mthod in home to show them in dropdown list
                     $http.get("Home/ReturnCountries")
                        .then(function (response) {
                            $scope.countries = response.data;
                        })
                     // the ng.submith had a function which called sendform(), it will be treated here after the form is submitted
                     $scope.sendform = function () {

                         $http({
                                    method: "POST",
                                    url: "../home/NewPerson",
                             // data: $scope.form saves every data that is set to the form and we have set some data 
                             // to it in our creat person form under ng-model
                                    data: $scope.form
                                })
                                .success(function (data) {
                                    if (data.errors) {
                                        console.log(data.errors)
                                    } else {
                                        alert("Posted successfully");
                                    }
                                    // assign destinatoin url to our location that we already defined it in function
                                    $location.url('/list')
                                })
                     }
                 })

    //in irder to path the id to the methods we use $routeParams 
                 .controller("detailController", function ($scope, $http, $routeParams) {
                     $scope.params = $routeParams;

                     $http.get("Home/AjaxThatReturnsJsonPerson/" + $scope.params.ID)
                        .then(function (response) {
                            $scope.detail = response.data;
                            $scope.message = "Person detail"
                            $scope.delmessage = "Confirm deleting"
                        })

                 })


                 .controller("editController", function ($scope, $http, $location, $routeParams) {
                     $scope.paramss = $routeParams;
                     $http.get("Home/AjaxThatReturnsJsonPerson/" + $scope.paramss.ID)
                        .then(function (response) {
                            $scope.detailToEdit = response.data;
                        })

                     $http.get("Home/ReturnCountries")
                        .then(function (response) {
                            $scope.countriesEdit = response.data;
                        })

                     $scope.sendform = function () {
                         alert('saving changes...');
                         console.log($scope.detailToEdit);
                         $http({
                             method: "POST",
                             url: "../home/Edit",
                             data: $scope.detailToEdit
                         })
                                .success(function (data) {
                                    if (data.errors) {
                                        console.log(data.errors)
                                    } else {
                                        alert("edited successfully");
                                    }
                                    $location.url('/list')
                                })
                     }
                 })

                 .controller("deleteController", function ($scope, $http, $routeParams, $location) {
                     $scope.params = $routeParams;

                     $http.get("Home/DeleteConfirmed/" + $scope.params.ID)
                        .then(function (response) {
                            $scope.detail = response.data;
                            $location.url('/list')
                        })

                 })
