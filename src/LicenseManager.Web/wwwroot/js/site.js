$(function() {
    init();
    function init() {
        ko.applyBindings(new ViewModel());
    };

    function ViewModel()
    {
        var self = this;
        self.licenses = ko.observableArray([]);

        
        loadLicenses();
        function loadLicenses() {
            $.get("http://localhost:5000/licenses", function(response){
                self.licenses(response);
            })
        }
    }
})();