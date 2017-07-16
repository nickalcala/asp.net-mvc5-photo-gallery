// Infinite Scroll
var pageNumber = 1;
var isPageLoading = false;
var hasMorePhotos = true;

// HandleBars
var source = null;
var template = null;
var photosContainer = null;

(function () {

    photosContainer = $('#photos-container');
    source = $('#photo-handlebars-template').html();
    template = Handlebars.compile(source);

    loadPhotos();

    window.onscroll = function () {
        
        if ((window.innerHeight + window.pageYOffset) >= document.body.offsetHeight
            && !isPageLoading) {

            if (!hasMorePhotos) {
                alert('No more photos!');
                return false;
            }
            
            loadPhotos();
        }
    };
    
})();

function setupTagButtons() {
    
    $('.tag-photo-button').on('click', function () {
        var $this = $(this);
        var id = $this.data('photo-id');
        var $tagPhotoContainer = $('#tag-photo-container-' + id);

        $tagPhotoContainer.show();
        $tagPhotoContainer.find('input').focus();

        $this.hide();
    });

    $('.tag-photo-cancel-button').on('click', function () {
        var $this = $(this);
        var id = $this.data('photo-id');
        var $tagPhotoContainer = $('#tag-photo-container-' + id);
        var $tagPhotoButton = $('#tag-photo-button-' + id);

        $tagPhotoContainer.hide();
        $tagPhotoButton.show();
    });

    $('.tag-photo-save-button').on('click', function () {
        var $this = $(this);
        var id = $this.data('photo-id');
        var $tagPhotoContainer = $('#tag-photo-container-' + id);
        var $tagPhotoButton = $('#tag-photo-button-' + id);
        var $tagPhotoInput = $('#tag-photo-input-' + id);
        
        $.ajax({
            url: '/Photos/TagPhoto/' + id,
            type: 'post',
            data: {
                __RequestVerificationToken: $("input[name=__RequestVerificationToken]").val(),
                tags: $tagPhotoInput.tagsinput('items')
            },
            success: function (response) {
                alert('Successfully tagged photo!');
            }
        });

        $tagPhotoContainer.hide();
        $tagPhotoButton.show();
    });

}

function setupInputTags() {
    $('.tag-photo-input').tagsinput();
}

function loadPhotos() {

    isPageLoading = true;

    $.getJSON('/Photos/Page/' + pageNumber, function (response) {

        hasMorePhotos = response.data.length >= 10;
        if (hasMorePhotos) {
            pageNumber++;
        }
        
        $.each(response.data, function (index, photo) {

            var dt = new Date(parseInt(photo.CreatedAt.replace("/Date(", "").replace(")/", ""), 10));
            photo.CreatedAt = $.timeago(dt);

            var photoHtml = template(photo);
            photosContainer.append(photoHtml);
            
        });

        setupTagButtons();
        setupInputTags();

        isPageLoading = false;
    });

}