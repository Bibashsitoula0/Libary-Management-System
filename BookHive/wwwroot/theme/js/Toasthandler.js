

function SucessMessage(message) {

    debugger;

    // Get the current URL
    var currentUrl = window.location.href;

    // Remove the query parameter
    var updatedUrl = removeQueryParam(currentUrl, 'message');

    // Update the URL without the query parameter
    window.history.replaceState({}, document.title, updatedUrl);

    // Function to remove a query parameter from a URL
    function removeQueryParam(url, param) {
        var urlParts = url.split('?');
        if (urlParts.length >= 2) {
            var prefix = encodeURIComponent(param) + '=';
            var params = urlParts[1].split(/[&;]/g);

            // Loop through the parameters and remove the one with the specified name
            for (var i = params.length; i-- > 0;) {
                if (params[i].lastIndexOf(prefix, 0) !== -1) {
                    params.splice(i, 1);
                }
            }

            // Reconstruct the URL
            return urlParts[0] + (params.length > 0 ? '?' + params.join('&') : '');
        }
        return url;
    }
    return toastr.success(message);

  

   
}

function WarningMessage(message) {
    // Get the current URL
    var currentUrl = window.location.href;

    // Remove the query parameter
    var updatedUrl = removeQueryParam(currentUrl, 'message');

    // Update the URL without the query parameter
    window.history.replaceState({}, document.title, updatedUrl);

    // Function to remove a query parameter from a URL
    function removeQueryParam(url, param) {
        var urlParts = url.split('?');
        if (urlParts.length >= 2) {
            var prefix = encodeURIComponent(param) + '=';
            var params = urlParts[1].split(/[&;]/g);

            // Loop through the parameters and remove the one with the specified name
            for (var i = params.length; i-- > 0;) {
                if (params[i].lastIndexOf(prefix, 0) !== -1) {
                    params.splice(i, 1);
                }
            }

            // Reconstruct the URL
            return urlParts[0] + (params.length > 0 ? '?' + params.join('&') : '');
        }
        return url;
    }
    return toastr.warning(message);
}

function DangerMessage(message) {

    // Get the current URL
    var currentUrl = window.location.href;

    // Remove the query parameter
    var updatedUrl = removeQueryParam(currentUrl, 'message');

    // Update the URL without the query parameter
    window.history.replaceState({}, document.title, updatedUrl);

    // Function to remove a query parameter from a URL
    function removeQueryParam(url, param) {
        var urlParts = url.split('?');
        if (urlParts.length >= 2) {
            var prefix = encodeURIComponent(param) + '=';
            var params = urlParts[1].split(/[&;]/g);

            // Loop through the parameters and remove the one with the specified name
            for (var i = params.length; i-- > 0;) {
                if (params[i].lastIndexOf(prefix, 0) !== -1) {
                    params.splice(i, 1);
                }
            }

            // Reconstruct the URL
            return urlParts[0] + (params.length > 0 ? '?' + params.join('&') : '');
        }
        return url;
    }
    return toastr.error(message);
}