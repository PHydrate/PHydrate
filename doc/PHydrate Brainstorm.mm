<map version="0.9.0">
<!-- To view this file, download free mind mapping software FreeMind from http://freemind.sourceforge.net -->
<node CREATED="1286920051971" ID="ID_1211383138" MODIFIED="1286920066009" TEXT="PHydrate Brainstorm">
<node CREATED="1286920066031" ID="ID_651783484" MODIFIED="1286920078496" POSITION="right" TEXT="Map by convention"/>
<node CREATED="1286920079006" ID="ID_662450132" MODIFIED="1286920087888" POSITION="right" TEXT="Use attributes to determine sprocs to call">
<node CREATED="1286920122754" ID="ID_1185575947" MODIFIED="1286920149734" TEXT="On Class">
<node CREATED="1286920149735" ID="ID_370279339" MODIFIED="1286920178819" TEXT="[HydrateProcedure(&quot;SelectProcName&quot;)]">
<node CREATED="1286920447905" ID="ID_648668528" MODIFIED="1286920454623" TEXT="Maybe just [Hydrate()]"/>
<node CREATED="1286999727198" ID="ID_1625937873" MODIFIED="1286999829651" TEXT="[HydrateUsing()]"/>
</node>
<node CREATED="1286920179509" ID="ID_396735795" MODIFIED="1286920482100" TEXT="[PersistProcedure(&quot;CreateProcName&quot;)]">
<node CREATED="1286920457281" ID="ID_1801905842" MODIFIED="1286920462976" TEXT="Maybe just [Persist()]"/>
<node CREATED="1286920567142" ID="ID_700675501" MODIFIED="1286920570932" TEXT="Or [Create()]"/>
<node CREATED="1286999853059" ID="ID_1116845692" MODIFIED="1286999856710" TEXT="[CreateUsing()]"/>
<node CREATED="1286920571638" ID="ID_1633580908" MODIFIED="1286920575510" TEXT="Or [Insert()]"/>
</node>
<node CREATED="1286920205862" ID="ID_649554711" MODIFIED="1286920232245" TEXT="[PersistProcedure(Create = &quot;CreateProc&quot;, Update = &quot;UpdateProc&quot;, Delete = &quot;DeleteProc&quot;)]">
<node CREATED="1286999766751" ID="ID_1357541562" MODIFIED="1286999840073" TEXT="[PersistUsing()]"/>
</node>
<node CREATED="1286920498595" ID="ID_184996519" MODIFIED="1286920503442" TEXT="[DeleteProcedure()]">
<node CREATED="1286999783535" ID="ID_1490795846" MODIFIED="1286999819747" TEXT="[DeleteUsing()]"/>
</node>
<node CREATED="1286920504578" ID="ID_581014742" MODIFIED="1286920508722" TEXT="[UpdateProcedure()]">
<node CREATED="1286999793808" ID="ID_410394473" MODIFIED="1286999824820" TEXT="[UpdateUsing()]"/>
</node>
<node CREATED="1287077532831" ID="ID_1902339780" MODIFIED="1287077537662" TEXT="[AggregateRoot]">
<node CREATED="1287077537663" ID="ID_1549564748" MODIFIED="1287077550699" TEXT="Signifies that the class is the aggregate root"/>
<node CREATED="1287077550966" ID="ID_246127359" MODIFIED="1287077769960" TEXT="Get&lt;T&gt; should only allow T to be a type that has an AggregateRoot">
<node CREATED="1287077769961" FOLDED="true" ID="ID_280346049" MODIFIED="1287092764423" TEXT="Is there a way to check this at compile time?">
<node CREATED="1287078566163" ID="ID_212385876" MODIFIED="1287078586326" TEXT="Doesn&apos;t look like it.  Might not be worth the effort to put in without it."/>
</node>
<node CREATED="1287077782978" FOLDED="true" ID="ID_1938313357" MODIFIED="1287092764007" TEXT="Should this be an optional feature?">
<node CREATED="1287077793752" ID="ID_367749024" MODIFIED="1287077800531" TEXT="If so, should it be on or off by default?"/>
</node>
</node>
<node CREATED="1287078754603" ID="ID_1007700271" MODIFIED="1287078759629" TEXT="Persist&lt;T&gt; also"/>
</node>
<node CREATED="1287083538747" ID="ID_914881715" MODIFIED="1287091413972" TEXT="[Factory(typeof(T))]">
<node CREATED="1287083550047" ID="ID_326755569" MODIFIED="1287083578627" TEXT="The IHydrationFactory&lt;T&gt; to be used to hydrate objects"/>
<node CREATED="1287083700466" ID="ID_299766822" MODIFIED="1287083716082" TEXT="Convention default: &lt;Class&gt;Factory??"/>
</node>
</node>
<node CREATED="1286920268793" ID="ID_725328175" MODIFIED="1287091416354" TEXT="On members">
<node CREATED="1286920273193" ID="ID_1014888440" MODIFIED="1286920293912" TEXT="[UniqueIdentifier]"/>
<node CREATED="1286920296730" ID="ID_1554658980" MODIFIED="1286920378817" TEXT="[References&lt;T&gt;(LazyLoad = true)]">
<node CREATED="1286920311341" ID="ID_1211980402" MODIFIED="1287092736137" TEXT="Can you use generic attributes?">
<node CREATED="1286997708297" ID="ID_1539940924" MODIFIED="1286997716377" TEXT="No, not in 2.0 at least.">
<node CREATED="1287078594068" ID="ID_71474778" MODIFIED="1287078601639" TEXT="Looks like 4.0 doesn&apos;t allow it either"/>
</node>
<node CREATED="1286997716841" ID="ID_1933133546" MODIFIED="1286997727896" TEXT="Can use References(typeof(T)) instead"/>
</node>
<node CREATED="1287081610069" ID="ID_1999523274" MODIFIED="1287081613528" TEXT="Many-to-one"/>
<node CREATED="1286920346605" ID="ID_661551847" MODIFIED="1286920351519" TEXT="[LazyLoad] ?"/>
</node>
<node CREATED="1287081585140" ID="ID_484977517" MODIFIED="1287081597480" TEXT="[CollectionOf(typeof(T))]">
<node CREATED="1287081604229" ID="ID_696831844" MODIFIED="1287081606935" TEXT="One-to-many"/>
<node CREATED="1287081619813" ID="ID_898942660" MODIFIED="1287081634120" TEXT="member type should be ICollection&lt;T&gt;"/>
</node>
<node CREATED="1286920389646" ID="ID_258261422" MODIFIED="1286920408001" TEXT="[ParameterName(&quot;Name&quot;)]">
<node CREATED="1286920408002" ID="ID_1560597155" MODIFIED="1286921200560" TEXT="With or without the leading @?"/>
<node CREATED="1286920413359" ID="ID_1719875960" MODIFIED="1286920432126" TEXT="Need a way to set a default prefix (ie &quot;Parameter_&quot;)">
<node CREATED="1286920608615" ID="ID_1401747772" MODIFIED="1286920620376" TEXT="On session factory initialization"/>
<node CREATED="1286920688523" ID="ID_24290536" MODIFIED="1286920693081" TEXT="As a class attribute"/>
<node CREATED="1286920800432" ID="ID_789531037" MODIFIED="1286920808671" TEXT="Need to be able to override default"/>
</node>
</node>
<node CREATED="1286920636554" ID="ID_129183556" MODIFIED="1286997972757" TEXT="[ColumnName(&quot;Name&quot;)]">
<node CREATED="1286920642380" ID="ID_1535592199" MODIFIED="1286921189393" TEXT="Needed if the names don&apos;t map exactly"/>
</node>
<node CREATED="1286997973802" ID="ID_1847074279" MODIFIED="1286999933929" TEXT="[Recordset(1)]">
<node CREATED="1286997987790" ID="ID_1232948520" MODIFIED="1286998020313" TEXT="Identifies 0-indexed recordset returned as data source"/>
</node>
<node CREATED="1286999937399" ID="ID_432475918" MODIFIED="1286999954264" TEXT="[IdentifiesType]">
<node CREATED="1287079129435" ID="ID_1387464450" MODIFIED="1287079141774" TEXT="Needed to be able to figure out what concrete type to instantiate"/>
<node CREATED="1287079191822" ID="ID_134984552" MODIFIED="1287079201297" TEXT="Is there a convention that we can use here?"/>
<node CREATED="1287083511042" ID="ID_249371152" MODIFIED="1287083528928" TEXT="Can implement IHydrationFactory&lt;T&gt;"/>
</node>
</node>
<node CREATED="1286993745676" ID="ID_510593714" MODIFIED="1286993751199" TEXT="Things to remember">
<node CREATED="1286993751199" ID="ID_1972463361" MODIFIED="1286993762714" TEXT="Need to support polymorphism"/>
<node CREATED="1286993764202" ID="ID_1518991098" MODIFIED="1286995028306" TEXT="Support multiple recordsets"/>
<node CREATED="1286998953541" ID="ID_1304679828" MODIFIED="1286998965630" TEXT="Support persisting entire Aggregates"/>
</node>
</node>
<node CREATED="1286921517007" ID="ID_994012574" MODIFIED="1286921519585" POSITION="right" TEXT="Interfaces">
<node CREATED="1286984810308" FOLDED="true" ID="ID_580667733" MODIFIED="1287081520620" TEXT="Interface design options">
<node CREATED="1286921519586" ID="ID_384277580" MODIFIED="1286921543326" TEXT="Reuse the NHibernate ISession[Factory]?">
<node CREATED="1286921543871" ID="ID_1509669329" MODIFIED="1286921548752" TEXT="Advantages">
<node CREATED="1286921549232" ID="ID_732941546" MODIFIED="1286921555232" TEXT="Drop in replacement">
<node CREATED="1286922197163" ID="ID_735284234" MODIFIED="1286922203946" TEXT="Not really, mapping files go away."/>
</node>
</node>
<node CREATED="1286922115827" ID="ID_358699327" MODIFIED="1286922118902" TEXT="Disadvantages">
<node CREATED="1286922118903" ID="ID_300809327" MODIFIED="1286922126929" TEXT="Need to reference another library"/>
</node>
</node>
<node CREATED="1286922129666" ID="ID_579653507" MODIFIED="1286922142243" TEXT="Copy ISession[Factory], but not reuse?">
<node CREATED="1286922142243" ID="ID_44429551" MODIFIED="1286922145570" TEXT="Advantages">
<node CREATED="1286922145571" ID="ID_1654439473" MODIFIED="1286922152014" TEXT="Decouples from NHibernate"/>
<node CREATED="1286922184907" ID="ID_103685099" MODIFIED="1286922196059" TEXT="Almost a drop-in replacement"/>
</node>
<node CREATED="1286922153823" ID="ID_1928665131" MODIFIED="1286922156580" TEXT="Disadvantages">
<node CREATED="1286922156581" ID="ID_435211684" MODIFIED="1286922168331" TEXT="Licensing?"/>
</node>
</node>
<node CREATED="1286922207306" ID="ID_1232693991" MODIFIED="1286984838376" TEXT="From-scratch design">
<icon BUILTIN="button_ok"/>
<node CREATED="1286922229341" ID="ID_406330597" MODIFIED="1286922239496" TEXT="Probably will still be similar to NHibernate (at least the factory)"/>
<node CREATED="1286922240120" ID="ID_250406750" MODIFIED="1286922255655" TEXT="Can implement a more fluent interface out of the box"/>
<node CREATED="1286984857046" ID="ID_1408806517" MODIFIED="1286995452314" TEXT="Use different name than Session?">
<node CREATED="1286984877031" ID="ID_1425595450" MODIFIED="1286995245382" TEXT="Maybe Session is accurate?">
<node CREATED="1286995289379" ID="ID_203757408" MODIFIED="1286995339376" TEXT="Don&apos;t like Session because it could be confused with NHibernate&apos;s interface"/>
<node CREATED="1286995344224" ID="ID_1613318192" MODIFIED="1286995362576" TEXT="It might make developers more comfortable with switching"/>
</node>
<node CREATED="1286995245910" ID="ID_253146164" MODIFIED="1286995281156" TEXT="Shouldn&apos;t be any of the ADO concepts"/>
</node>
</node>
</node>
<node CREATED="1286984829557" ID="ID_1703568769" MODIFIED="1286984834562" TEXT="Interface design">
<node CREATED="1286997575602" ID="ID_964577699" MODIFIED="1286997578615" TEXT="ISessionFactory">
<node CREATED="1286997578616" ID="ID_39304342" MODIFIED="1286997593120" TEXT="ISession GetSession()"/>
<node CREATED="1287078666935" ID="ID_1843124292" MODIFIED="1287079088244" TEXT="Global transaction handling?">
<node CREATED="1286997593679" ID="ID_48956609" MODIFIED="1286997614767" TEXT="void StartTransaction()"/>
<node CREATED="1286997615262" ID="ID_520341100" MODIFIED="1286997621646" TEXT="void CommitTransaction()"/>
<node CREATED="1286997624126" ID="ID_1306689284" MODIFIED="1286997633956" TEXT="void RollbackTransaction()"/>
</node>
<node CREATED="1287081254998" ID="ID_55493816" MODIFIED="1287081284604" TEXT="static BuildSessionFactory(IConfiguration)"/>
</node>
<node CREATED="1286997635597" ID="ID_1350559053" MODIFIED="1286997638241" TEXT="ISession">
<node CREATED="1286997638242" ID="ID_1770452719" MODIFIED="1286997763926" TEXT="T Get&lt;T&gt;(Action&lt;T&gt;)">
<node CREATED="1286997834312" ID="ID_246299139" MODIFIED="1286997843926" TEXT="Queries based on values set in the action">
<node CREATED="1286997843926" ID="ID_957465783" MODIFIED="1286998328423" TEXT="Requires that members have public setters"/>
<node CREATED="1286997858608" ID="ID_848324581" MODIFIED="1286998341974" TEXT="Might not be desirable, especially for read-only values like generated Ids, is there another way?"/>
<node CREATED="1287079827145" ID="ID_410178928" MODIFIED="1287079849724" TEXT="Default HydrationFactory will need public setters anyway."/>
</node>
</node>
<node CREATED="1286997775061" ID="ID_1550261557" MODIFIED="1286997881264" TEXT="T Get&lt;T&gt;(params Object)">
<node CREATED="1286997792105" ID="ID_1453817618" MODIFIED="1286997831075" TEXT="Queries based on member(s) tagged with UniqueIdentifierAttribute"/>
</node>
<node CREATED="1286997871040" ID="ID_553916446" MODIFIED="1286997928445" TEXT="void Persist&lt;T&gt;(T)">
<node CREATED="1286997899651" ID="ID_807384461" MODIFIED="1286999015047" TEXT="Saves changed aggregates"/>
</node>
<node CREATED="1287078700809" ID="ID_88475165" MODIFIED="1287078713067" TEXT="ITransaction Transaction {get}">
<node CREATED="1287078645286" ID="ID_1991774192" MODIFIED="1287078725068" TEXT="void Begin()"/>
<node CREATED="1287078649750" ID="ID_1833375584" MODIFIED="1287078729163" TEXT="void Commit()"/>
<node CREATED="1287078654535" ID="ID_407622651" MODIFIED="1287078733597" TEXT="void Rollback()"/>
</node>
</node>
<node CREATED="1287079855770" ID="ID_185411499" MODIFIED="1287079858816" TEXT="IHydrationFactory">
<node CREATED="1287079858817" ID="ID_1064253972" MODIFIED="1287079865741" TEXT="For default implementation"/>
<node CREATED="1287079873515" ID="ID_1861612949" MODIFIED="1287079906671" TEXT="Object Hydrate(Type, IDictionary)"/>
</node>
<node CREATED="1287079620945" ID="ID_142736382" MODIFIED="1287081036816" TEXT="IHydrationFactory&lt;out T&gt; : IHydrationFactory">
<node CREATED="1287079638185" ID="ID_1307546670" MODIFIED="1287079648515" TEXT="Optional"/>
<node CREATED="1287092649107" ID="ID_370691262" MODIFIED="1287092665797" TEXT="This will need .NET 4.0."/>
<node CREATED="1287079648753" ID="ID_77902523" MODIFIED="1287079754648" TEXT="If configured, use this factory to build objects of type T."/>
<node CREATED="1287079665986" ID="ID_672645847" MODIFIED="1287079685589" TEXT="Allows decoupling of the creation from the code"/>
<node CREATED="1287079782663" ID="ID_485774368" MODIFIED="1287079796074" TEXT="Auto-load implementations from an assembly at startup time?">
<node CREATED="1287083591694" ID="ID_1212560206" MODIFIED="1287091413973" TEXT="Can be an attribute on the class">
<arrowlink DESTINATION="ID_914881715" ENDARROW="Default" ENDINCLINATION="699;0;" ID="Arrow_ID_631427029" STARTARROW="None" STARTINCLINATION="699;0;"/>
</node>
</node>
<node CREATED="1287080212026" ID="ID_1400079527" MODIFIED="1287080222768" TEXT="How do we keep these internally?">
<node CREATED="1287081061214" ID="ID_1338850838" MODIFIED="1287091341796" TEXT="IDictionary&lt;Type, IHydrationFactory&lt;Object&gt;&gt;"/>
<node CREATED="1287083744765" ID="ID_608136930" MODIFIED="1287083751967" TEXT="May not be necessary"/>
</node>
<node CREATED="1287083893925" ID="ID_1712625270" MODIFIED="1287091348655" TEXT="Implementations will need to have default constructors">
<node CREATED="1287083943186" ID="ID_384337766" MODIFIED="1287083948420" TEXT="But they should be singletons">
<node CREATED="1287083976464" ID="ID_1963720063" MODIFIED="1287083983427" TEXT="They would not necessarily have to be."/>
<node CREATED="1287084875150" ID="ID_1034794700" MODIFIED="1287084890256" TEXT="Would there ever be a case where we might not want it to be a singleton?"/>
<node CREATED="1287091179312" ID="ID_662797974" MODIFIED="1287091189633" TEXT="Actually, we probably don&apos;t want the application to care"/>
</node>
<node CREATED="1287084731351" ID="ID_685085431" MODIFIED="1287091341796" TEXT="use an internal HydrationFactoryRepository to handle this">
<arrowlink DESTINATION="ID_1338850838" ENDARROW="Default" ENDINCLINATION="164;0;" ID="Arrow_ID_694170485" STARTARROW="None" STARTINCLINATION="164;0;"/>
</node>
<node CREATED="1287084950474" ID="ID_1283517504" MODIFIED="1287085022746" TEXT="could use convention and look for an Instance property"/>
<node CREATED="1287085357075" ID="ID_143146363" MODIFIED="1287091356781" TEXT="Something like:">
<node CREATED="1287085360796" ID="ID_1254000989" MODIFIED="1287090983930" TEXT="HydrationFactorySingleton&lt;T,U&gt; : IHydrationFactory&lt;T&gt; where U : IHydrationFactory&lt;T&gt;, new() {&#xa;    private U _instance;&#xa;    private HydrationFactorySingleton(){}&#xa;&#xa;    public static Instance {get {}}&#xa;&#xa;    public T Hydrate(IDictionary params) {&#xa;        return _instance.Hydrate(params);&#xa;    }&#xa;}"/>
<node CREATED="1287090881521" ID="ID_1028290367" MODIFIED="1287090926475" TEXT="public class Singleton&lt;T&gt; where T : class, new()&#xa;{&#xa;    public static readonly T Instance = new T();&#xa;}"/>
<node CREATED="1287091300880" ID="ID_1557681351" MODIFIED="1287091312390" TEXT="These are just silly.  Just cache them in the repository."/>
</node>
</node>
<node CREATED="1287079913757" ID="ID_1809966007" MODIFIED="1287079921472" TEXT="T Hydrate(IDictionary)"/>
</node>
</node>
</node>
</node>
</map>
