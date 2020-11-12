$(document).ready(function () {
  var trigger = $('.hamburger'),
      overlay = $('.overlay'),
     isClosed = false;

    trigger.click(function () {
      hamburger_cross();      
    });

    function hamburger_cross() {

      if (isClosed == true) {          
        overlay.hide();
        trigger.removeClass('is-open');
        trigger.addClass('is-closed');
        isClosed = false;
      } else {   
        overlay.show();
        trigger.removeClass('is-closed');
        trigger.addClass('is-open');
        isClosed = true;
      }
  }
  
  $('[data-toggle="offcanvas"]').click(function () {
        $('#wrapper').toggleClass('toggled');
  }); 
  setTimeout(function(){
    $('.alert_r').remove();
  },2000);
  
    var x= $('#list').val();
    console.log(x); 

    $('.carousel').carousel({
        interval: 5000 //changes the speed
    });
    $('#server').click(function(){
      $('#url').show();
      $('.dd').show();
     });


    $("#onward a").click(function(){
      var onwardId = $(this).attr("href");
      $("html, body").animate({scrollTop: $(onwardId).offset().top}, "slow");
      return false;
    });
    $('#close').click(function(){
      $('#fix').remove();
    });
   

});