jQuery(document).ready(function($) {
  $('#ajaxbutton').click(LoadFDHView);

  function LoadFDHView()
  {
    alert('function called');
    $.ajax({
      url: Drupal.settings.basePath + '/views/ajax',
      type: 'post',
      data: {
        view_name: 'fdh',
        view_display_id: 'ajax_test_view', //your display id
        view_args: {['field_fdh_square_forty_value']:'179'}, // your views arguments
      },
      dataType: 'json',
      success: function (response) {
        if (response[1] !== undefined) {
          var viewHtml = response[1].data;
          $('.view-content').html(viewHtml);
          
        }
      }
    });
  }
});
