function countryCtrl($scope) {
    //$scope содержит данные модели. Это связующее звено между контроллером и видом.
    //$scope всего лишь один из сервисов, внедренных в контроллер.
    $scope.country = {
        name: 'Ukraine',
        area: '603 628',
        population: '42 825 883',
        capital: {
            name: 'Kiev'
        }
    };
}