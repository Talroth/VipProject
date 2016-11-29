var app = angular.module('vip', ['ngMaterial','md.data.table']);
app.controller('vipController', ['$scope', '$http', '$location', '$mdDialog', '$mdToast','$timeout', function ($scope, $http, $location, $mdDialog, $mdToast,$timeout) {

    $scope.Headers = [{ "name": 'Id' }, { "name": 'VIP name' }, { "name": 'Age' }, { "name": 'Country' }];

    // ofcourse this should be taken from some DB or other repo but in order to keep the code simple i implement it like that
    $scope.countries = [ "Israel","USA", "France","Australia","New Zealand","Brazil"];

    var path = 'http://' + $location.host() + ':' + $location.port() + '/api/Vipdata'; 

    /* get all vip list by call Get service without arguments*/
    $scope.getVipData = function () {
        $http({
            url: path ,
            method: 'GET',
            content: 'application/json',
            accepts: 'application/json'
        }).success(function (response) {
            $scope.vipList = response;

        }).error(function (response) {
            $scope.errorToast();
        });
    }

    // update vip by passing vip object to VipdataController/Put
    $scope.updateVip = function(vip) {	
        $http({
            url: path, 
            method: 'PUT',  
            data: vip,
            accepts: 'text/plain',
        }).success(function(response) {
            $scope.openToast(vip.name + " was updated");

        }).error(function(response) {
            $scope.errorToast();
        });   
 
    }

    // remove vip by passing vip id to VipdataController/DELETE
    $scope.removeVip = function (vipId) {
        return $http({
            url: path , 
            method: 'DELETE',  
            data: vipId,
            content: 'application/json',
            accepts: 'text/plain'
        }).success(function(response) {

        }).error(function(response) {
            $scope.errorToast();
        });   
	 
    }

    // create vip by passing vip object to VipdataController/POST
    $scope.createVip = function(vip) {	
        return $http({
            url: path , 
            method: 'POST',  
            data: vip,
            accepts: 'application/json'
        }).success(function(response) {
            
        }).error(function(response) {
            $scope.errorToast();
        }); 
	 
    }


    /* Dialog */

    // Create new VIP dialog

    $scope.showCreateDialog = function($event) {

        var parentEl = angular.element(document.body);
        $mdDialog.show({
            parent: parentEl,
            scope: $scope,
            preserveScope: true,
            targetEvent: $event,
            templateUrl:
                'createVip.html',
            controller: DialogController
        })
     
	 
    function DialogController($scope, $mdDialog) {	      
        $scope.closeDialog = function () {
            $scope.newName = '';
            $scope.newAge = null;
            $scope.newCountry = null;
            $mdDialog.hide();
        }
        $scope.AddCustomer = function(newName,newAge, newCountry)
        {
            this.newVip = {name : newName, age : newAge, country : newCountry}; 
    	         	       
            $scope.createVip(this.newVip).then(function (response) {
                $scope.vipList = $scope.vipList.concat(response.data);
                $scope.openToast(response.data.name + " was added");
            },function(error) { 
                    $scope.openToast(error.data.message);
            });
    	    	   
            $scope.newName = '';
            $scope.newAge = null;
            $scope.newCountry = null;
            $mdDialog.hide(); 
        }
    	      
    }
            
}

 // Delete customer dialog/
  
	  $scope.removeDialog = function($event, Index, id,list) {
	      var parentEl = angular.element(document.body);
	      $mdDialog.show({
	        parent: parentEl,
	        scope: $scope,
	        preserveScope: true,
	        targetEvent: $event,
	        templateUrl:
               'removeVip.html',
	        controller: DialogController
	     })
	     
	      
	     function DialogController($scope, $mdDialog) {
 
	         delName = list[Index].name;
	       $scope.closeDialog = function() {
	         $mdDialog.hide();
	       }
	       
	       $scope.Remove = function()
	       {
	    		  	
		      $scope.removeVip(id).then(function(response){
		          list.splice(Index, 1);
		          
		          $scope.openToast(delName + " was removed successfully " );
	       },function(error) {
	      			$scope.errorToast();
	       });
	    	  $mdDialog.hide(); 
	    	   
	       }
	   }	            
	 }

	  
/* Toast affect function */
  $scope.openToast = function(msg) {
      $mdToast.show(
              $mdToast.simple()
                 .textContent(msg)                       
                 .hideDelay(3000)
           );
	  };
	
  
  $scope.errorToast = function() {
	  $scope.openToast("Problem with the server connection, please try to login again");
  }
  
}]);

// allow angularjs to send  DELETE for JSON format
app.config(function($httpProvider) {
	  $httpProvider.defaults.headers["delete"] = {
	      'Content-Type': 'application/json;charset=utf-8' 

	  };
	}

)
