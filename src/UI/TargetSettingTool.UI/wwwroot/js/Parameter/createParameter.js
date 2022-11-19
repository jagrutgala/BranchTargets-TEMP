$.ajax({
  url: "/ParameterType/GetAllParameterTypesJson",
  type: "GET",
  success: function (d) {
    console.table(d);
    $.each(d, function (i, para) {
      $("#ParameterType").append(
        $("<option></option>")
          .addClass(`${para.id}`)
          .val(para.id)
          .html(para.name)
      );
    });
  },
  error: function () {
    alert("Error!");
  },
});

function validateStartDate() {
  const startdate = new Date($("#StartDate").val());
  const errorStartDate = $("#error-StartDate");
  if (isNaN(startdate)) {
    errorStartDate.text("Start date is required"); // error msg
    return false;
  }
  return true;
}
function resetErrorStartDate() {
  const errorStartDate = $("#error-StartDate");
  errorStartDate.text("");
}

function validateEndDate() {
  const enddate = new Date($("#EndDate").val());
  const errorEndDate = $("#error-EndDate");
  if (isNaN(enddate)) {
    errorEndDate.text("End date is required"); // error msg
    return false;
  }
  const startdate = new Date($("#StartDate").val());
  if (startdate > enddate) {
    errorEndDate.text("End date need to be bigger then start date");
    return false;
  }
  return true;
}
function resetErrorEndDate() {
  const errorEndDate = $("#error-EndDate");
  errorEndDate.text("");
}

function validateFinancialYear() {
  const startdate = new Date($("#StartDate").val());
  const enddate = new Date($("#EndDate").val());
  const financialYearElement = document.querySelector("#FinancialYear");
  if (!(validateStartDate() && validateEndDate())) {
    return false;
  }
  let finyear = "";
  if (startdate.getMonth() >= 03) {
    finyear = `${startdate.getFullYear().toString()}-${(
      enddate.getFullYear() + 1
    )
      .toString()
      .substring(2)}`;
  } else {
    finyear = `${(startdate.getFullYear() - 1).toString()}-${enddate
      .getFullYear()
      .toString()
      .substring(2)}`;
  }
  financialYearElement.readOnly = false;
  financialYearElement.value = finyear.toString();
  financialYearElement.readOnly = true;
  return true;
}
function resetErrorFinancialYear() {
  const errorFinancialYear = $("#error-FinancialYear");
  errorFinancialYear.text("");
}

function validateParameterType() {
  const parameterSelect = document.querySelector("#ParameterType");
  const errorParameterType = $("#error-ParameterType");
  if (parameterSelect.value == null || parameterSelect.value == "") {
    errorParameterType.text("Please select a parameter type"); // error msg
    return false;
  }
  return true;
}
function resetErrorParameterType() {
  const errorParameterType = $("#error-ParameterType");
  errorParameterType.text("");
}

function validateTargetAmount() {
  const targetAmount = $("#TargetAmount");
  const errorTargetAmount = $("#error-TargetAmount");

  if (targetAmount.val() == null || targetAmount.val() == "") {
    errorTargetAmount.text("Target Amount is required"); // error msg
    return false;
  }
  if (targetAmount.val() <= 0) {
    errorTargetAmount.text("Please enter a amount greater than or equal to 1"); // error msg
    return false;
  }
  return true;
}
function resetErrorTargetAmount() {
  const errorTargetAmount = $("#error-TargetAmount");
  errorTargetAmount.text(""); // error msg
}

function validateSelectedParameterType() {
  const parameterTypeList = $("#ParameterType option:not(:first-child).hide");
  const errorParameterType = $("#error-ParameterType");
  if (parameterTypeList.length <= 0) {
    errorParameterType.text("Please select atleast one parameter type"); // error msg
    return false;
  }
}
function resetErrorSelectedParameterType() {
  const errorParameterType = $("#error-ParameterType");
  errorParameterType.text("");
}

function addParameter() {
  let paraForm = `
                    <tr>
                        <td data-parameter></td>
                        <td data-target></td>
                        <td action>
                          <button type="button">
                              <i class="fa-solid fa-trash"></i>
                          </button>
                        </td>
                    </tr>
                `;
  let newTarget = $(paraForm);

  const parameterSelect = $("#ParameterType");
  const targetAmountInput = $("#TargetAmount");
  const paramId = parameterSelect.find(":selected").val();
  const paramName = parameterSelect.find(":selected").text();
  const targetAmt = targetAmountInput.val();

  newTarget.addClass("target");
  newTarget.addClass(`parameter-${paramId}`);

  newTarget.find("td[data-parameter]").text(`${paramName}`);
  newTarget.find("td[data-parameter]").data("data-parameter", `${paramId}`);
  newTarget.find("td[data-target]").text(`${targetAmt}`);
  newTarget.find("td[data-target]").data("data-target", `${targetAmt}`);
  newTarget.find("td[data-target]").text(`${targetAmt}`);

  newTarget.find("button").addClass("btn");
  newTarget.find("button").on("click", (event) => {
    parameterSelect.find(`.${paramId}`).removeClass("hide");
    newTarget.remove();
    if (document.querySelector("#TargetList").childElementCount <= 0) {
      document.querySelector("#StartDate").readOnly = false;
      document.querySelector("#EndDate").readOnly = false;
    }
  });

  parameterSelect.find(`[data-default-option]`).prop("selected", true);
  parameterSelect.find(`.${paramId}`).addClass("hide");
  targetAmountInput.val("");

  newTarget.appendTo("#TargetList");

  if (document.querySelector("#TargetList").childElementCount > 0) {
    document.querySelector("#StartDate").readOnly = true;
    document.querySelector("#EndDate").readOnly = true;
  }
}

function formatData() {
  const startDate = $("#StartDate").val();
  const endDate = $("#EndDate").val();
  const financialYear = $("#FinancialYear").val();
  const prametersTargets = [];
  const parameterTypeList = $(".target")
    .toArray()
    .forEach((element, index) => {
      console.log(element);
      prametersTargets.push({
        targetAmount: $(element).find("td[data-target]").data("data-target"),
        parameterTypeId: $(element)
          .find("td[data-parameter]")
          .data("data-parameter"),
      });
    });

  return {
    startDate,
    endDate,
    financialYear,
    prametersTargets,
  };
}

$("#addParaBtn").on("click", () => {
  const validateArray = [validateParameterType(), validateTargetAmount()];
  if (validateArray.some((v) => v === false)) {
    return false;
  }
  addParameter();
});

$("#StartDate").on("input", () => {
  if (validateStartDate()) {
    resetErrorStartDate();
  }
  if (validateFinancialYear()) {
    document.querySelector("#addParaBtn").disabled = false;
  } else {
    document.querySelector("#addParaBtn").disabled = true;
  }
});
$("#EndDate").on("input", () => {
  if (validateEndDate()) {
    resetErrorEndDate();
  }
  if (validateFinancialYear()) {
    document.querySelector("#addParaBtn").disabled = false;
  } else {
    document.querySelector("#addParaBtn").disabled = true;
  }
});

$("#parameterTypeSelect").on("input", () => {
  if (validateParameterType()) {
    resetErrorParameterType();
  }
});
$("#TargetAmount").on("input", () => {
  if (validateTargetAmount()) {
    resetErrorTargetAmount();
  }
});

$("#saveBtn").on("click", (event) => {
  const validateArray = [
    validateStartDate(),
    validateEndDate(),
    validateFinancialYear(),
    validateSelectedParameterType(),
  ];
  if (validateArray.some((v) => v === false)) {
    return false;
  }
  const data = formatData();
  console.log(data);
  $.post(
    "/Parameter/CreateParameter",
    {
      model: data,
      parametersTargets: data.prametersTargets,
    },
    () => {
      window.location.assign("/Parameter/GetAllParameters");
    }
  );
});
