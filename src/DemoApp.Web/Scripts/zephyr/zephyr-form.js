!function ($) {

    $('.btn[type="submit"]').on('click', function() {
        if ($('form').valid())
            $(this).button('loading');
    });

    //chosen plugin for dropdowns
    $(".chzn-select").chosen();
    $(".chzn-select-deselect").chosen({ allow_single_deselect: true }); 

} (window.jQuery);