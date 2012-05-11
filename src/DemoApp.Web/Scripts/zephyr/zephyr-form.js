!function ($) {

    $('form').on('submit', function() {
        if ($('form').valid())
            $('.btn[type="submit"]').button('loading');
    });

    //chosen plugin for dropdowns
    $(".chzn-select").chosen();
    $(".chzn-select-deselect").chosen({ allow_single_deselect: true }); 

} (window.jQuery);