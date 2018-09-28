package com.productScanner.services;

import com.productScanner.model.ImageClasificationEventResultEntry;
import com.productScanner.model.ImagePreprocessingEventResult;
import com.productScanner.model.ImagePreprocessingEventResultEntry;
import openllet.jena.PelletReasonerFactory;
import openllet.query.sparqldl.jena.SparqlDLExecutionFactory;
import org.apache.jena.datatypes.xsd.XSDDatatype;
import org.apache.jena.ontology.Individual;
import org.apache.jena.ontology.OntClass;
import org.apache.jena.ontology.OntModel;
import org.apache.jena.query.*;
import org.apache.jena.rdf.model.*;

import java.io.FileNotFoundException;
import java.io.PrintWriter;
import java.util.HashMap;
import java.util.Map;

public class WebSemanticService {
    private OntModel _model = ModelFactory.createOntologyModel(PelletReasonerFactory.THE_SPEC);
    private final String iri = "http://www.product-scanner/ontology";
    private final String ontologyFileName = "knowledge-base.owl";

    public WebSemanticService() {
        loadData();
    }

    private void loadData() {
        System.out.println("Loading knowledge base from RDF file");
        _model.read(ontologyFileName, "OWL/XML-ABBREV");
    }

    public void addToOntology(ImageClasificationEventResultEntry[] entryList, int id) {
        for (ImageClasificationEventResultEntry entry : entryList) {
            addToOntology(entry, id);
        }
    }

    public void save() {
        try (PrintWriter out = new PrintWriter(ontologyFileName)) {
            _model.write(out);
        } catch (FileNotFoundException e) {
            e.printStackTrace();
        }
    }

    public int getTotalNumberOfInvalidIndividuals() {
        String totalInvalidNumberIndividualsQuery = "PREFIX rdfs: <http://www.w3.org/2000/01/rdf-schema#>\n" +
                "PREFIX ont: <http://www.product-scanner/ontology#>\n" +
                "SELECT (count(?individuals) as ?total)\n" +
                "WHERE{" +
                "?cls rdfs:subClassOf ont:Invalid." +
                "?individuals a ?cls." +
                "?individuals a ont:Product}";
        return readIndividuals(totalInvalidNumberIndividualsQuery);
    }

    public int getTotalNumberOfIndividuals() {
        String totalIndividualsQuery = "PREFIX rdfs: <http://www.w3.org/2000/01/rdf-schema#>\n" +
                "PREFIX ont: <http://www.product-scanner/ontology#>\n" +
                "SELECT  distinct ?totalIndividuals\n" +
                "WHERE{\t\n" +
                "\t?total rdfs:subClassOf ont:Product.\n" +
                "\t?totalIndividuals a ?total }";
        return readIndividuals(totalIndividualsQuery);
    }

    public Map<String, Integer> getNumberOfDetectedIndividuals() {
        String queryString = "PREFIX rdfs: <http://www.w3.org/2000/01/rdf-schema#>\n" +
                "PREFIX ont: <http://www.product-scanner/ontology#>\n" +
                "PREFIX rdf: <http://www.w3.org/1999/02/22-rdf-syntax-ns#>\n" +
                "SELECT ?name (count(?name) as ?total)\n" +
                "WHERE {\n" +
                "{ ?x a ont:CocaCola . ?x rdf:type ?name. ?name rdfs:subClassOf ont:CocaCola} UNION \n" +
                "{ ?x a ont:Pepsi.   ?x rdf:type ?name. ?name rdfs:subClassOf ont:Pepsi} UNION \n" +
                "{ ?x a ont:CocaColaZero.  ?x rdf:type ?name. ?name rdfs:subClassOf ont:CocaColaZero} .\n" +
                "}\n" +
                "group by ?name";
        return readIndividualCounter(queryString);
    }

    private Map<String, Integer> readIndividualCounter(String queryString) {
        Query query = QueryFactory.create(queryString);
        QueryExecution qexec = SparqlDLExecutionFactory.create(query, _model);
        HashMap<String, Integer> result = new HashMap<>();
        ResultSet resultSet = qexec.execSelect();
        while (resultSet.hasNext()) {
            QuerySolution soln = resultSet.nextSolution();
            System.out.println(soln.toString());
            String name = EscapeReasonerResult(soln.getResource("name").toString());
            String valueString = soln.getLiteral("total").toString();
            String escapedValue = GetNumberFromResult(valueString);
            result.put(name, Integer.parseInt(escapedValue));
        }
        return result;
    }

    private int readIndividuals(String queryString) {
        Query query = QueryFactory.create(queryString);
        QueryExecution qexec = SparqlDLExecutionFactory.create(query, _model);
        ResultSet result = qexec.execSelect();
        int counter = 0;
        while (result.hasNext()) {
            QuerySolution soln = result.nextSolution();
            counter++;
        }
        return counter;
    }


    public void addToOntology(ImageClasificationEventResultEntry entry, int imageId) {
        OntClass product = _model.getOntClass(iri + "#Product");
        Individual individual = _model.createIndividual(iri + "#" + entry.Id, product);

        //setup image id
        Property hasImageId = _model.createProperty(iri + "#hasImageId");
        individual.addProperty(hasImageId, String.valueOf(imageId), XSDDatatype.XSDint);

        //setup name
        Property hasName = _model.createProperty(iri + "#productName");
        individual.addProperty(hasName, entry.Category, XSDDatatype.XSDstring);

        //setup position
        Property hasPositionYMin = _model.createProperty(iri + "#positionYMin");
        individual.addProperty(hasPositionYMin, String.valueOf(entry.Position[0]), XSDDatatype.XSDdouble);

        Property hasPositionXMin = _model.createProperty(iri + "#positionXMin");
        individual.addProperty(hasPositionXMin, String.valueOf(entry.Position[1]), XSDDatatype.XSDdouble);

        Property hasPositionYMax = _model.createProperty(iri + "#positionYMax");
        individual.addProperty(hasPositionYMax, String.valueOf(entry.Position[2]), XSDDatatype.XSDdouble);

        Property hasPositionXmax = _model.createProperty(iri + "#positionXMax");
        individual.addProperty(hasPositionXmax, String.valueOf(entry.Position[3]), XSDDatatype.XSDdouble);
    }

    public void startResoing() {
        _model.prepare();
    }

    public ImagePreprocessingEventResult getResult(ImageClasificationEventResultEntry[] entryList, int id) {
        ImagePreprocessingEventResult preprocesingResult = new ImagePreprocessingEventResult();
        preprocesingResult.Id = id;

        for (ImageClasificationEventResultEntry entry : entryList) {
            Resource reasonerResource = _model.getResource(iri + "#" + entry.Id);
            StmtIterator iter = reasonerResource.listProperties();

            ImagePreprocessingEventResultEntry dataEntry = new ImagePreprocessingEventResultEntry();
            dataEntry.Id = entry.Id;

            while (iter.hasNext()) {

                Statement stmt = iter.nextStatement();  // get next statement
                Property predicate = stmt.getPredicate();   // get the predicate
                RDFNode object = stmt.getObject();      // get the object
                String predicateString = EscapeReasonerResult(predicate.toString());

                switch (predicateString) {
                    case "differentFrom":
                    case "sameAs":
                    case "hasImageId":
                    case "positionYMax":
                    case "positionYMin":
                    case "positionXMax":
                    case "positionXMin":
                        continue;
                    case "type":
                        String value = EscapeReasonerResult(object.toString());
                        if (value.equals("Thing")) {
                            continue;
                        }
                        dataEntry.Types.add(value);
                        break;
                    default:
                        dataEntry.Data.put(
                                predicateString,
                                EscapeReasonerResult(object.toString()));
                        break;
                }
            }
            preprocesingResult.Data.add(dataEntry);
        }
        return preprocesingResult;
    }

    private String EscapeReasonerResult(String result) {
        int indexOfHash = result.lastIndexOf("#");
        // +1 because we want to ignore #
        return result.substring(indexOfHash + 1);
    }

    private String GetNumberFromResult(String result) {
        int indexOfDash = result.indexOf("^");
        return result.substring(0, indexOfDash);
    }
}
