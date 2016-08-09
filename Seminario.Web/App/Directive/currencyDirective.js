var app = angular.module('app');
app.directive('blurToCurrency', function($filter){
    return {
        scope: {
            amount  : '='
        },
        link: function(scope, el, attrs){
            el.val($filter('currency')(scope.amount));
      
            el.bind('focus', function(){
                el.val(scope.amount);
            });
      
            el.bind('input', function(){
                scope.amount = el.val();
                scope.$apply();
            });
      
            el.bind('blur', function(){
                el.val($filter('currency')(scope.amount));
            });
        }
    }
});