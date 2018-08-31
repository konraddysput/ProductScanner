package com.productScanner.semanticModel;

import org.apache.jena.rdf.model.Model;
import org.apache.jena.rdf.model.Property;
import org.apache.jena.rdf.model.Resource;

import java.lang.reflect.Field;
import java.util.Collection;
import java.util.Collections;
import java.util.List;

public abstract  class ModelBase {
    protected  String uri;
    public  String classifier;

    public ModelBase(String uri, String classifier){
        this.uri = uri;
        this.classifier = classifier;
    }

    public String getUri(){
        return  uri + classifier;
    }

    public Resource loadResources(Resource resource, Model model) throws IllegalAccessException {
        //avoid private or protected fields
        Field[] fields = getClass().getFields();
        for (Field field : fields) {
            field.setAccessible(true);
            Property modelProperty = model.createProperty(uri, field.getName());

            if (Collection.class.isAssignableFrom(field.getType())){

                Resource nestedResource = model.createResource(this.getUri() + "/"+ field.getName() );
                int index=  0;
                for (ModelBase val : ((List<ModelBase>)field.get(this))) {

                    Resource listResource = model.createResource(this.getUri() + "/"+ field.getName() + "/" + index);
                    listResource= val.loadResources(listResource, model);
                    nestedResource.addProperty(modelProperty, listResource);
                    index++;
                }
                resource.addProperty(modelProperty, nestedResource);
                continue;
            }
            resource.addProperty(modelProperty, field.get(this).toString());
        }
        return resource;
    }

}
