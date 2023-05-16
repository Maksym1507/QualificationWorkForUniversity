import { observer } from "mobx-react-lite";
import { FC, ReactElement, useEffect } from "react";
import { Button, Spinner } from "react-bootstrap";
import { useNavigate, useParams } from "react-router-dom";
import { basketStore, catalogStore } from "../../App";
import BasketItemModel from "../../models/basketItemModel";
import NoMatchComponent from "../../components/NoMatch/NoMatchComponent";

const CatalogItemComponent: FC<any> = observer((): ReactElement => {
  const { id } = useParams();
  const navigation = useNavigate();

  useEffect(() => {
    (async () => {
      if (id) {
        await catalogStore.getSingleCatalogItem(id);
      }
    })();
  }, [id]);

  if (catalogStore.singleCatalogItem) {
    if (catalogStore.singleCatalogItem.id) {
      return (
        <>
          <div className="container-sm mt-2">
            {catalogStore.isLoading ? (
              <div className="container">
                <div className="row min-vh-100">
                  <div className="col d-flex flex-column justify-content-center align-items-center">
                    <Spinner animation="border" />
                  </div>
                </div>
              </div>
            ) :
              (
                <section className="my-4">
                  <div className="container px-4 px-lg-5">
                    <div className="row gx-4 gx-lg-5">
                      <div className="col-md-4">
                        <img
                          className="card-img-top mb-5 mb-md-0"
                          src={catalogStore.singleCatalogItem.pictureUrl}
                          alt={catalogStore.singleCatalogItem.title}
                        />
                      </div>
                      <div className="col-md-6">
                        <h1 className="display-4 fw-bolder mb-2">{catalogStore.singleCatalogItem.title}</h1>
                        <div className="fs-5 mb-2">
                          <span>{catalogStore.singleCatalogItem.price} uah</span>
                        </div>
                        <p className="mb-3"><span className="fw-bolder">Ingredients: </span>{catalogStore.singleCatalogItem.description}</p>
                        <div className="d-flex mb-2"><span className="fw-bolder">Weight:&nbsp;</span>{catalogStore.singleCatalogItem.weight}g</div>
                        <div className="d-flex mt-5">
                          <Button
                            onClick={() => {
                              navigation(-1);
                            }}
                            className="me-3 btn-info"
                          >
                            Back to shop
                          </Button>
                          <button
                            className="btn btn-outline-dark flex-shrink-0"
                            onClick={async () =>
                              await basketStore.addItem({
                                id: catalogStore.singleCatalogItem.id,
                                product: catalogStore.singleCatalogItem,
                                price: catalogStore.singleCatalogItem.price,
                                count: 1,
                              } as BasketItemModel)
                            }
                          >
                            Add to cart
                          </button>
                        </div>
                      </div>
                    </div>
                  </div>
                </section>
              )}
          </div>
        </>
      );
    }
  }

  return <NoMatchComponent />;
});

export default CatalogItemComponent;