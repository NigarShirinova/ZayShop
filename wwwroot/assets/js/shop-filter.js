document.addEventListener("DOMContentLoaded", function () {
    const categoryLinks = document.querySelectorAll(".templatemo-accordion a");
    const productContainer = document.querySelector(".row");

    categoryLinks.forEach(link => {
        link.addEventListener("click", function (event) {
            event.preventDefault();

            const categoryId = this.dataset.categoryId;

            fetch('/Shop/FilterProducts', {
                method: 'POST',
                headers: {
                    'Content-Type': 'application/json'
                },
                body: JSON.stringify({ categoryId: parseInt(categoryId) })
            })
                .then(response => response.json())
                .then(products => {
                    productContainer.innerHTML = "";

                    if (products.length === 0) {
                        productContainer.innerHTML = "<p>No products found for this category.</p>";
                    } else {
                        products.forEach(product => {
                            productContainer.innerHTML += `
                            <div class="col-md-4">
                                <div class="card mb-4 product-wap rounded-0">
                                    <div class="card rounded-0">
                                        <img class="card-img rounded-0 img-fluid" src="${product.PhotoPath}">
                                        <div class="card-img-overlay rounded-0 product-overlay d-flex align-items-center justify-content-center">
                                            <ul class="list-unstyled">
                                                <li><a class="btn btn-success text-white" href="#"><i class="far fa-heart"></i></a></li>
                                                <li><a class="btn btn-success text-white mt-2" href="#"><i class="far fa-eye"></i></a></li>
                                                <li><a class="btn btn-success text-white mt-2" href="#"><i class="fas fa-cart-plus"></i></a></li>
                                            </ul>
                                        </div>
                                    </div>
                                    <div class="card-body">
                                        <a href="#" class="h3 text-decoration-none">${product.Name}</a>
                                        <p class="text-center mb-0">$${product.Price}</p>
                                    </div>
                                </div>
                            </div>
                        `;
                        });
                    }
                })
                .catch(error => console.error('Error:', error));
        });
    });
});
