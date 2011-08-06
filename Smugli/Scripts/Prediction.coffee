viewModel =
	prediction: ko.observable ""
	predictionHref: ko.observable ""
	makePrediction: ->
		$.ajax {
			url: "/Predictions",
			type: "POST",
			data: (JSON.stringify {Prediction: @prediction()}),
			dataType: "json",
			contentType: "application/json",
			success: (data, textStatus, jqXHR) ->
				viewModel.predictionHref jqXHR.getResponseHeader("Location")
			}

viewModel.predictionLink = ko.dependentObservable ->
	uri = new URI viewModel.predictionHref()
	uri.resolve new URI window.location.toString()