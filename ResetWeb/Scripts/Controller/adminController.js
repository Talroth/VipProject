var app = angular.module('vip', ['ngMaterial','md.data.table']);
app.controller('vipController', ['$scope', '$http', '$location', '$mdDialog', '$mdToast','$rootScope','$timeout','$window', function ($scope, $http, $location, $mdDialog, $mdToast, $rootScope,$timeout,$window) {

    $scope.customerHeaders = [{ "name": 'Id' }, { "name": 'VIP name' }, { "name": 'Age' }, { "name": 'Country' }];
    $scope.countries = [{ "name" : "Israel", "name" : "USA", "name" :"France" }];

    var path = 'http://' + $location.host() + ':' + $location.port() + '/api/Vipdata'; 

    /* get all vip list */
    $scope.getVipData = function () {
        $http({
            url: path ,
            method: 'GET',
            content: 'application/json',
            accepts: 'application/json'
        }).success(function (response) {
            $scope.vipList = response;

        }).error(function (response) {
            console.log("error occurred.");
            $scope.openToast("Problem with the server connection, please try to login again")
        });
    }


    $scope.updateVip = function(vip) {	
        $http({
            url: path, 
            method: 'PUT',  
            data: vip,
            accepts: 'text/plain',
        }).success(function(response) {
            $scope.openToast(vip.name + " was updated")

        }).error(function(response) {
            $scope.errorToast();
        });   
 
    }

    $scope.removeVip = function (vipId) {
        return $http({
            url: path , 
            method: 'DELETE',  
            data: vipId,
            'content-type': 'application/json',
            'accepts-type': 'text/plain'
        }).success(function(response) {

        }).error(function(response) {
            $scope.errorToast();
        });   
	 
    }

    /* need to put 'return' at the beginning in order to use promise later */
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


    $scope.showCreateDialog = function($event) {

        var parentEl = angular.element(document.body);
        $mdDialog.show({
            parent: parentEl,
            scope: $scope,
            preserveScope: true,
            targetEvent: $event,
            template:
              '<md-dialog aria-label="List dialog">' +
              '  <md-dialog-content>'+
            '<md-input-container class="md-block" flex-gt-sm>' +
            '<label>VIP name</label>'  +
            '<input ng-model="newName">' +
              '</md-input-container>' +
              '<md-input-container class="md-block" flex-gt-sm>' +
              '<label>Age</label>'  +
                      '<input type="number" ng-model="newAge">' +
              '</md-input-container>' +
              '<md-input-container class="md-block" flex-gt-sm>' +
              '<label>Country</label>' +
                      '<input ng-model="newCountry">' +
              '</md-input-container>' +
              '  </md-dialog-content>' +
              '  <md-dialog-actions>' +
              '    <md-button ng-click="closeDialog()" class="md-primary">' +
              '      Close Dialog' +
              '    </md-button>' +
              '    <md-button ng-click="AddCustomer(newName,newAge, newCountry)" class="md-primary">' +
              '      Add' +
              '    </md-button>' +
              '  </md-dialog-actions>' +
              '</md-dialog>',

            controller: DialogController
        })
     
	 
    function DialogController($scope, $mdDialog) {

	      
        $scope.closeDialog = function() {
            $mdDialog.hide();
        }
        $scope.AddCustomer = function(newName,newAge, newCountry)
        {
            this.newLocalCustomer = {name : newName, age : newAge, country : newCountry}; 
            console.log("+ " + newName + "," + newAge);
    	         	       
            $scope.createVip(this.newLocalCustomer).then(function(response) {
                $scope.vipList = $scope.vipList.concat(response.data);
                $scope.openToast(response.data.name + " was added");
            },function(error) { 
                    $scope.openToast(error.data.message);
            });
    	    	   
            $scope.newName = '';
            $scope.newAge = ''; 
            $scope.newCountry = '';
            $mdDialog.hide(); 
        }
    	      
    }
            
}

 /* Delete customer dialog */
  
	  $scope.removeDialog = function($event, Index, id,list) {
	      var parentEl = angular.element(document.body);
	      $mdDialog.show({
	        parent: parentEl,
	        scope: $scope,
	        preserveScope: true,
	        targetEvent: $event,
	        template:
	          '<md-dialog aria-label="List dialog">' +
	          '  <md-dialog-content>'+
	      	'<md-input-container class="md-block" flex-gt-sm>' +
	          '<label>Would you like to delete the customer?</label>' +
	          '  <md-dialog-actions>' +
	          '    <md-button ng-click="closeDialog()" class="md-primary">' +
	          '      No' +
	          '    </md-button>' +
	          '    <md-button ng-click="Remove(id)" class="md-primary">' +
	          '      Yes' +
	          '    </md-button>' +
	          '  </md-dialog-actions>' +
	          '</md-dialog>',
	        controller: DialogController
	     })
	     
	      
	     function DialogController($scope, $mdDialog) {
 
	       var individualText;
	       $scope.closeDialog = function() {
	         $mdDialog.hide();
	       }
	       
	       $scope.Remove = function()
	       {
	    		  	
		      $scope.removeVip(id).then(function(response){
		      list.splice(Index,1);
		      $scope.openToast("Removal was performed successfully"); 
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

// allow angularjs to send for DELETE JSON format
app.config(function($httpProvider) {
	  /**
	   * make delete type json
	   */
	  $httpProvider.defaults.headers["delete"] = {
	      'Content-Type': 'application/json;charset=utf-8' 

	  };
	}

)
