namespace ProsperousUniverse.API;

public class ActionNames
{
    /* System */
    public const string ActionCompleted = "ACTION_COMPLETED";
    public const string SubscriptionsUpdate = "SUBSCRIPTIONS_UPDATE";
    public const string SimulationGetSimulationData = "SIMULATION_GET_SIMULATION_DATA";
    public const string SimulationData = "SIMULATION_DATA";
    public const string PresenceList = "PRESENCE_LIST";
    public const string GameFinishLoading = "GAME_FINISH_LOADING";
    
    /* System - Connection */
    public const string ClientConnectionOpened = "CLIENT_CONNECTION_OPENED";
    public const string ClientConnectionClosed = "CLIENT_CONNECTION_CLOSED";
    public const string ClientConnectionReconnecting = "CLIENT_CONNECTION_RECONNECTING";
    public const string ClientConnectionReconnectFailed = "CLIENT_CONNECTION_RECONNECT_FAILED";
    public const string ServerConnectionOpened = "SERVER_CONNECTION_OPENED";
    public const string ServerConnectionClosed = "SERVER_CONNECTION_CLOSED";
    
    /* Accounting */
    public const string AccountingBookings = "ACCOUNTING_BOOKINGS";
    public const string AccountingGetBalances = "ACCOUNTING_GET_BALANCES";
    public const string AccountingBalances = "ACCOUNTING_BALANCES";
    public const string AccountingGetCashBookings = "ACCOUNTING_GET_CASH_BOOKINGS";
    public const string AccountingCashBookings = "ACCOUNTING_CASH_BOOKINGS";
    
    /* Admin center */
    public const string AdminCenterStartRun = "ADMIN_CENTER_START_RUN";
    public const string AdminCenterVote = "ADMIN_CENTER_VOTE";
    public const string AdminCenterWithdrawRun = "ADMIN_CENTER_WITHDRAW_RUN";
    public const string AdminCenterClientGetVotingData = "ADMIN_CENTER_CLIENT_GET_VOTING_DATA";
    public const string AdminCenterClientVotingData = "ADMIN_CENTER_CLIENT_VOTING_DATA";
    public const string AdminCenterGetAvailableLocalRuleChangeCount = "ADMIN_CENTER_GET_AVAILABLE_LOCAL_RULE_CHANGE_COUNT";
    public const string AdminCenterAvailableLocalRuleChangeCount = "ADMIN_CENTER_AVAILABLE_LOCAL_RULE_CHANGE_COUNT";
    
    /* Alerts */
    public const string AlertsGetAlerts = "ALERTS_GET_ALERTS";
    public const string AlertsAlerts = "ALERTS_ALERTS";
    public const string AlertsAlert = "ALERTS_ALERT";
    public const string AlertsAlertsDeleted = "ALERTS_ALERTS_DELETED";
    public const string AlertsMarkAsSeen = "ALERTS_MARK_AS_SEEN";
    public const string AlertsMarkAsRead = "ALERTS_MARK_AS_READ";
    
    /* Authentication */
    public const string AuthAuthenticate = "AUTH_AUTHENTICATE";
    public const string AuthAuthenticated = "AUTH_AUTHENTICATED";
    public const string AuthUnauthenticated = "AUTH_UNAUTHENTICATED";
    public const string AuthImpersonate = "AUTH_IMPERSONATE";
    public const string AuthImpersonated = "AUTH_IMPERSONATED";
    
    /* Blueprints */
    public const string BlueprintGetBlueprints = "BLUEPRINT_GET_BLUEPRINTS";
    public const string BlueprintBlueprints = "BLUEPRINT_BLUEPRINTS";
    public const string BlueprintBlueprint = "BLUEPRINT_BLUEPRINT";
    public const string BlueprintCreateBlueprint = "BLUEPRINT_CREATE_BLUEPRINT";
    public const string BlueprintChangeOptionSelection = "BLUEPRINT_CHANGE_OPTION_SELECTION";
    public const string BlueprintSaveSelection = "BLUEPRINT_SAVE_SELECTION";
    public const string BlueprintDiscardSelection = "BLUEPRINT_DISCARD_SELECTION";
    public const string BlueprintDelete = "BLUEPRINT_DELETE";
    public const string BlueprintCopy = "BLUEPRINT_COPY";
    public const string BlueprintRename = "BLUEPRINT_RENAME";
    
    /* Chat */
    public const string ChannelGetData = "CHANNEL_GET_DATA";
    public const string ChannelData = "CHANNEL_DATA";
    public const string ChannelAddMessage = "CHANNEL_ADD_MESSAGE";
    public const string ChannelChannelList = "CHANNEL_CHANNEL_LIST";
    public const string ChannelCreateConversation = "CHANNEL_CREATE_CONVERSATION";
    public const string ChannelGetChannelList = "CHANNEL_GET_CHANNEL_LIST";
    public const string ChannelGetMessageList = "CHANNEL_GET_MESSAGE_LIST";
    public const string ChannelGetReadStatus = "CHANNEL_GET_READ_STATUS";
    public const string ChannelGetUserList = "CHANNEL_GET_USER_LIST";
    public const string ChannelJoin = "CHANNEL_JOIN";
    public const string ChannelJoined = "CHANNEL_JOINED";
    public const string ChannelLeave = "CHANNEL_LEAVE";
    public const string ChannelLeft = "CHANNEL_LEFT";
    public const string ChannelMessageAdded = "CHANNEL_MESSAGE_ADDED";
    public const string ChannelMessageList = "CHANNEL_MESSAGE_LIST";
    public const string ChannelMute = "CHANNEL_MUTE";
    public const string ChannelUnmute = "CHANNEL_UNMUTE";
    public const string ChannelReadStatus = "CHANNEL_READ_STATUS";
    public const string ChannelRename = "CHANNEL_RENAME";
    public const string ChannelRenamed = "CHANNEL_RENAMED";
    public const string ChannelStartTyping = "CHANNEL_START_TYPING";
    public const string ChannelStopTyping = "CHANNEL_STOP_TYPING";
    public const string ChannelStartedTyping = "CHANNEL_STARTED_TYPING";
    public const string ChannelStoppedTyping = "CHANNEL_STOPPED_TYPING";
    public const string ChannelUnseenMessagesCount = "CHANNEL_UNSEEN_MESSAGES_COUNT";
    public const string ChannelUpdateReadStatus = "CHANNEL_UPDATE_READ_STATUS";
    public const string ChannelResetUnseenMessagesCount = "CHANNEL_RESET_UNSEEN_MESSAGES_COUNT";
    public const string ChannelUserJoined = "CHANNEL_USER_JOINED";
    public const string ChannelUserLeft = "CHANNEL_USER_LEFT";
    public const string ChannelUserList = "CHANNEL_USER_LIST";
    public const string ChannelDeleteMessage = "CHANNEL_DELETE_MESSAGE";
    public const string ChannelMessageDeleted = "CHANNEL_MESSAGE_DELETED";
    
    /* Chat - Client */
    public const string ChannelClientMembership = "CHANNEL_CLIENT_MEMBERSHIP";
    public const string ChannelClientResolveMembership = "CHANNEL_CLIENT_RESOLVE_MEMBERSHIP";
    public const string ChannelClientGetUnseenMessagesCount = "CHANNEL_CLIENT_GET_UNSEEN_MESSAGES_COUNT";
    public const string ChannelClientAddUser = "CHANNEL_CLIENT_ADD_USER";
    
    /* Chat - Registry */
    public const string ChannelRegistryGetCatalog = "CHANNEL_REGISTRY_GET_CATALOG";
    public const string ChannelRegistryCatalog = "CHANNEL_REGISTRY_CATALOG";
    
    /* Company */
    public const string CompanyGetData = "COMPANY_GET_DATA";
    public const string CompanyData = "COMPANY_DATA";
    public const string CompanyHeadquarterAddUpgradeMaterials = "COMPANY_HEADQUARTER_ADD_UPGRADE_MATERIALS";
    public const string CompanyHeadquarterRelocate = "COMPANY_HEADQUARTER_RELOCATE";
    
    /* Commodity Exchange - Broker */
    public const string ComexGetBrokerData = "COMEX_GET_BROKER_DATA";
    public const string ComexTickerInvalid = "COMEX_TICKER_INVALID";
    public const string ComexBrokerData = "COMEX_BROKER_DATA";
    public const string ComexBrokerGetPrices = "COMEX_BROKER_GET_PRICES";
    public const string ComexBrokerPrices = "COMEX_BROKER_PRICES";
    public const string ComexBrokerNewPrice = "COMEX_BROKER_NEW_PRICE";
    public const string ComexPlaceOrder = "COMEX_PLACE_ORDER";
    public const string ComexDeleteOrder = "COMEX_DELETE_ORDER";
    
    /* Commodity Exchange - Exchanges */
    public const string ComexFindExchangeData = "COMEX_FIND_EXCHANGE_DATA";
    public const string ComexFindExchangeDataByAddress = "COMEX_FIND_EXCHANGE_DATA_BY_ADDRESS";
    public const string ComexExchangeData = "COMEX_EXCHANGE_DATA";
    public const string ComexGetExchangeList = "COMEX_GET_EXCHANGE_LIST";
    public const string ComexExchangeList = "COMEX_EXCHANGE_LIST";
    public const string ComexExchangeGetBrokerList = "COMEX_EXCHANGE_GET_BROKER_LIST";
    public const string ComexExchangeBrokerList = "COMEX_EXCHANGE_BROKER_LIST";
    
    /* Commodity Exchange - Trader */
    public const string ComexTraderGetOrders = "COMEX_TRADER_GET_ORDERS";
    public const string ComexTraderOrders = "COMEX_TRADER_ORDERS";
    public const string ComexTraderFindOrder = "COMEX_TRADER_FIND_ORDER";
    public const string ComexTraderOrder = "COMEX_TRADER_ORDER";
    public const string ComexTraderOrderAdded = "COMEX_TRADER_ORDER_ADDED";
    public const string ComexTraderOrderUpdated = "COMEX_TRADER_ORDER_UPDATED";
    public const string ComexTraderOrderRemoved = "COMEX_TRADER_ORDER_REMOVED";
    public const string ComexTraderGetOrderDeletionTerms = "COMEX_TRADER_GET_ORDER_DELETION_TERMS";
    public const string ComexTraderOrderDeletionTerms = "COMEX_TRADER_ORDER_DELETION_TERMS";
    
    /* Contracts */
    public const string ContractsGetContracts = "CONTRACTS_GET_CONTRACTS";
    public const string ContractsContracts = "CONTRACTS_CONTRACTS";
    public const string ContractsContract = "CONTRACTS_CONTRACT";
    public const string ContractsFulfillCondition = "CONTRACTS_FULFILL_CONDITION";
    public const string ContractsBreachContract = "CONTRACTS_BREACH_CONTRACT";
    public const string ContractsExtendContract = "CONTRACTS_EXTEND_CONTRACT";
    
    /* Contributions */
    public const string ContributionContribute = "CONTRIBUTION_CONTRIBUTE";
    
    /* Corporations */
    public const string CorporationGetData = "CORPORATION_GET_DATA";
    public const string CorporationData = "CORPORATION_DATA";
    public const string CorporationGetProjectsData = "CORPORATION_GET_PROJECTS_DATA";
    public const string CorporationProjectsData = "CORPORATION_PROJECTS_DATA";
    public const string CorporationFindProjectData = "CORPORATION_FIND_PROJECT_DATA";
    public const string CorporationProjectData = "CORPORATION_PROJECT_DATA";
    public const string CorporationProjectCancelled = "CORPORATION_PROJECT_CANCELLED";
    
    /* Corporations - Manager */
    public const string CorporationManagerGetBuildOptions = "CORPORATION_MANAGER_GET_BUILD_OPTIONS";
    public const string CorporationManagerBuildOptions = "CORPORATION_MANAGER_BUILD_OPTIONS";
    public const string CorporationManagerInviteCompany = "CORPORATION_MANAGER_INVITE_COMPANY";
    public const string CorporationManagerGetInvites = "CORPORATION_MANAGER_GET_INVITES";
    public const string CorporationManagerInvites = "CORPORATION_MANAGER_INVITES";
    public const string CorporationManagerInvite = "CORPORATION_MANAGER_INVITE";
    public const string CorporationManagerStartProject = "CORPORATION_MANAGER_START_PROJECT";
    public const string CorporationManagerCancelProject = "CORPORATION_MANAGER_CANCEL_PROJECT";
    
    /* Corporations - Shareholder */
    public const string CorporationShareholderFormCorporation = "CORPORATION_SHAREHOLDER_FORM_CORPORATION";
    public const string CorporationShareholderLeave = "CORPORATION_SHAREHOLDER_LEAVE";
    public const string CorporationShareholderGetHoldings = "CORPORATION_SHAREHOLDER_GET_HOLDINGS";
    public const string CorporationShareholderHoldings = "CORPORATION_SHAREHOLDER_HOLDINGS";
    public const string CorporationShareholderHoldingData = "CORPORATION_SHAREHOLDER_HOLDING_DATA";
    public const string CorporationShareholderGetInvites = "CORPORATION_SHAREHOLDER_GET_INVITES";
    public const string CorporationShareholderInvites = "CORPORATION_SHAREHOLDER_INVITES";
    public const string CorporationShareholderInvite = "CORPORATION_SHAREHOLDER_INVITE";
    public const string CorporationShareholderAcceptInvite = "CORPORATION_SHAREHOLDER_ACCEPT_INVITE";
    public const string CorporationShareholderRejectInvite = "CORPORATION_SHAREHOLDER_REJECT_INVITE";
    
    /* Countries */
    public const string CountryRegistryGetCountries = "COUNTRY_REGISTRY_GET_COUNTRIES";
    public const string CountryRegistryCountries = "COUNTRY_REGISTRY_COUNTRIES";
    
    /* Data */
    public const string DataGetData = "DATA_GET_DATA";
    public const string DataData = "DATA_DATA";
    public const string DataDataRemoved = "DATA_DATA_REMOVED";
    public const string DataResolveObjectPath = "DATA_RESOLVE_OBJECT_PATH";
    public const string DataObjectPath = "DATA_OBJECT_PATH";
    
    /* Experts */
    public const string ExpertsFindExperts = "EXPERTS_FIND_EXPERTS";
    public const string ExpertsExperts = "EXPERTS_EXPERTS";
    public const string ExpertsActivateExpert = "EXPERTS_ACTIVATE_EXPERT";
    public const string ExpertsDeactivateExpert = "EXPERTS_DEACTIVATE_EXPERT";
    
    /* Foreign Exchange - General */
    public const string ForexGetCurrencyPairs = "FOREX_GET_CURRENCY_PAIRS";
    public const string ForexCurrencyPairs = "FOREX_CURRENCY_PAIRS";
    public const string ForexBrokerFindData = "FOREX_BROKER_FIND_DATA";
    public const string ForexBrokerData = "FOREX_BROKER_DATA";
    public const string ForexBrokerPriceUpdate = "FOREX_BROKER_PRICE_UPDATE";
    public const string ForexBrokerGetPrices = "FOREX_BROKER_GET_PRICES";
    public const string ForexBrokerPrices = "FOREX_BROKER_PRICES";
    public const string ForexTraderCreateOrder = "FOREX_TRADER_CREATE_ORDER";
    public const string ForexTraderGetOrders = "FOREX_TRADER_GET_ORDERS";
    public const string ForexTraderOrders = "FOREX_TRADER_ORDERS";
    public const string ForexTraderDeleteOrder = "FOREX_TRADER_DELETE_ORDER";
    public const string ForexTraderFindOrder = "FOREX_TRADER_FIND_ORDER";
    public const string ForexTraderOrder = "FOREX_TRADER_ORDER";
    public const string ForexTraderOrderAdded = "FOREX_TRADER_ORDER_ADDED";
    public const string ForexTraderOrderUpdated = "FOREX_TRADER_ORDER_UPDATED";
    public const string ForexTraderOrderRemoved = "FOREX_TRADER_ORDER_REMOVED";
    
    /* Local market */
    public const string LocalMarketClientPostAd = "LOCAL_MARKET_CLIENT_POST_AD";
    public const string LocalMarketClientGetOwnAds = "LOCAL_MARKET_CLIENT_GET_OWN_ADS";
    public const string LocalMarketClientOwnAds = "LOCAL_MARKET_CLIENT_OWN_ADS";
    public const string LocalMarketClientGetAcceptedAds = "LOCAL_MARKET_CLIENT_GET_ACCEPTED_ADS";
    public const string LocalMarketClientAcceptedAds = "LOCAL_MARKET_CLIENT_ACCEPTED_ADS";
    public const string LocalMarketClientAd = "LOCAL_MARKET_CLIENT_AD";
    public const string LocalMarketClientDeleteAd = "LOCAL_MARKET_CLIENT_DELETE_AD";
    public const string LocalMarketClientAcceptAd = "LOCAL_MARKET_CLIENT_ACCEPT_AD";
    
    /* Local rules */
    public const string LocalRulesUpdateFeeRules = "LOCAL_RULES_UPDATE_FEE_RULES";
    public const string LocalRulesUpdatePopulationRules = "LOCAL_RULES_UPDATE_POPULATION_RULES";
    public const string LocalRulesGetGoverningEntityChoices = "LOCAL_RULES_GET_GOVERNING_ENTITY_CHOICES";
    public const string LocalRulesGoverningEntityChoices = "LOCAL_RULES_GOVERNING_ENTITY_CHOICES";
    public const string LocalRulesGetFeeLimitations = "LOCAL_RULES_GET_FEE_LIMITATIONS";
    public const string LocalRulesFeeLimitations = "LOCAL_RULES_FEE_LIMITATIONS";
    public const string LocalRulesUpdateGoverningEntity = "LOCAL_RULES_UPDATE_GOVERNING_ENTITY";
    
    /* Maps */
    public const string MapGetInitialGraphData = "MAP_GET_INITIAL_GRAPH_DATA";
    public const string MapInitialGraphData = "MAP_INITIAL_GRAPH_DATA";
    public const string MapGraphDataChanged = "MAP_GRAPH_DATA_CHANGED";
    public const string MapContextMenuShow = "MAP_CONTEXT_MENU_SHOW";
    public const string MapContextMenuHide = "MAP_CONTEXT_MENU_HIDE";
    public const string MapHighlightAddressShow = "MAP_HIGHLIGHT_ADDRESS_SHOW";
    public const string MapHighlightAddressHide = "MAP_HIGHLIGHT_ADDRESS_HIDE";
    public const string MapHighlightMissionShow = "MAP_HIGHLIGHT_MISSION_SHOW";
    public const string MapHighlightMissionHide = "MAP_HIGHLIGHT_MISSION_HIDE";
    
    /* Naming */
    public const string NamingNameEntity = "NAMING_NAME_ENTITY";
    
    /* Nomenclature */
    public const string NomenclatureQueryAddresses = "NOMENCLATURE_QUERY_ADDRESSES";
    public const string NomenclatureAddressesQueryData = "NOMENCLATURE_ADDRESSES_QUERY_DATA";
    
    /* Notifications */
    public const string NotificationsSetNotificationSettings = "NOTIFICATIONS_SET_NOTIFICATION_SETTINGS";
    public const string NotificationsGetConfig = "NOTIFICATIONS_GET_CONFIG";
    public const string NotificationsConfig = "NOTIFICATIONS_CONFIG";
    
    /* Planet */
    public const string PlanetFindData = "PLANET_FIND_DATA";
    public const string PlanetData = "PLANET_DATA";
    public const string PlanetGetSites = "PLANET_GET_SITES";
    public const string PlanetSites = "PLANET_SITES";
    public const string PlanetSiteCreated = "PLANET_SITE_CREATED";
    public const string PlanetSiteRemoved = "PLANET_SITE_REMOVED";
    
    /* Planetary Projects */
    public const string PlanetaryProjectCogcVote = "PLANETARY_PROJECT_COGC_VOTE";
    
    /* Population */
    public const string PopulationGetAvailableReserveWorkforce = "POPULATION_GET_AVAILABLE_RESERVE_WORKFORCE";
    public const string PopulationAvailableReserveWorkforce = "POPULATION_AVAILABLE_RESERVE_WORKFORCE";
    
    /* Production */
    public const string ProductionGetProductionLines = "PRODUCTION_GET_PRODUCTION_LINES";
    public const string ProductionProductionLines = "PRODUCTION_PRODUCTION_LINES";
    public const string ProductionGetSiteProductionLines = "PRODUCTION_GET_SITE_PRODUCTION_LINES";
    public const string ProductionSiteProductionLines = "PRODUCTION_SITE_PRODUCTION_LINES";
    public const string ProductionFindProductionLine = "PRODUCTION_FIND_PRODUCTION_LINE";
    public const string ProductionProductionLine = "PRODUCTION_PRODUCTION_LINE";
    public const string ProductionProductionLineAdded = "PRODUCTION_PRODUCTION_LINE_ADDED";
    public const string ProductionProductionLineUpdated = "PRODUCTION_PRODUCTION_LINE_UPDATED";
    public const string ProductionProductionLineRemoved = "PRODUCTION_PRODUCTION_LINE_REMOVED";
    public const string ProductionQueueOrder = "PRODUCTION_QUEUE_ORDER";
    public const string ProductionCancelOrder = "PRODUCTION_CANCEL_ORDER";
    public const string ProductionMoveOrder = "PRODUCTION_MOVE_ORDER";
    public const string ProductionOrderAdded = "PRODUCTION_ORDER_ADDED";
    public const string ProductionOrderUpdated = "PRODUCTION_ORDER_UPDATED";
    public const string ProductionOrderRemoved = "PRODUCTION_ORDER_REMOVED";
    
    /* Ships / Fleet / Navigation*/
    public const string ShipData = "SHIP_DATA";
    public const string ShipFindData = "SHIP_FIND_DATA";
    public const string ShipGetShips = "SHIP_GET_SHIPS";
    public const string ShipShips = "SHIP_SHIPS";
    public const string ShipFlightAbortFlight = "SHIP_FLIGHT_ABORT_FLIGHT";
    public const string ShipFlightCalculateMission = "SHIP_FLIGHT_CALCULATE_MISSION";
    public const string ShipFlightCalculateTestFlight = "SHIP_FLIGHT_CALCULATE_TEST_FLIGHT";
    public const string ShipFlightMissionReset = "SHIP_FLIGHT_MISSION_RESET";
    public const string ShipFlightMission = "SHIP_FLIGHT_MISSION";
    public const string ShipFlightCreateFlight = "SHIP_FLIGHT_CREATE_FLIGHT";
    public const string ShipFlightGetFlights = "SHIP_FLIGHT_GET_FLIGHTS";
    public const string ShipFlightFlights = "SHIP_FLIGHT_FLIGHTS";
    public const string ShipFlightFlight = "SHIP_FLIGHT_FLIGHT";
    public const string ShipFlightFlightEnded = "SHIP_FLIGHT_FLIGHT_ENDED";
    public const string ShipRename = "SHIP_RENAME";
    public const string ShipRenamed = "SHIP_RENAMED";
    public const string ShipRepair = "SHIP_REPAIR";
    
    /* Shipyards */
    public const string ShipyardCreateProject = "SHIPYARD_CREATE_PROJECT";
    public const string ShipyardGetProjects = "SHIPYARD_GET_PROJECTS";
    public const string ShipyardProjects = "SHIPYARD_PROJECTS";
    public const string ShipyardProject = "SHIPYARD_PROJECT";
    public const string ShipyardProjectAssignConstructionMaterial = "SHIPYARD_PROJECT_ASSIGN_CONSTRUCTION_MATERIAL";
    public const string ShipyardProjectStartProject = "SHIPYARD_PROJECT_START_PROJECT";
    
    /* Sites */
    public const string SiteCreateSite = "SITE_CREATE_SITE";
    public const string SiteGetSites = "SITE_GET_SITES";
    public const string SiteSites = "SITE_SITES";
    public const string SiteFind = "SITE_FIND";
    public const string SiteGetSiteByAddress = "SITE_GET_SITE_BY_ADDRESS";
    public const string SiteSite = "SITE_SITE";
    public const string SiteNoSite = "SITE_NO_SITE";
    public const string SiteSectionBuild = "SITE_SECTION_BUILD";
    public const string SitePlatformBuilt = "SITE_PLATFORM_BUILT";
    public const string SiteSectionDemolish = "SITE_SECTION_DEMOLISH";
    public const string SiteSectionRepair = "SITE_SECTION_REPAIR";
    public const string SitePlatformRemoved = "SITE_PLATFORM_REMOVED";
    public const string SitePlatformUpdated = "SITE_PLATFORM_UPDATED";
    public const string SiteChangeInvestedPermits = "SITE_CHANGE_INVESTED_PERMITS";
    
    /* Storage */
    public const string StorageChange = "STORAGE_CHANGE";
    public const string StorageGetStorages = "STORAGE_GET_STORAGES";
    public const string StorageStorages = "STORAGE_STORAGES";
    public const string StorageTransferItem = "STORAGE_TRANSFER_ITEM";
    public const string StorageTransferMaterials = "STORAGE_TRANSFER_MATERIALS";
    public const string StorageTransferStart = "STORAGE_TRANSFER_START";
    public const string StorageTransferEnd = "STORAGE_TRANSFER_END";
    public const string StorageRemoved = "STORAGE_REMOVED";
    
    /* System */
    public const string SystemGetStarsData = "SYSTEM_GET_STARS_DATA";
    public const string SystemStarsData = "SYSTEM_STARS_DATA";
    public const string SystemGetTraffic = "SYSTEM_GET_TRAFFIC";
    public const string SystemTraffic = "SYSTEM_TRAFFIC";
    public const string SystemTrafficShip = "SYSTEM_TRAFFIC_SHIP";
    public const string SystemTrafficShipRemoved = "SYSTEM_TRAFFIC_SHIP_REMOVED";
    
    /* Tutorials */
    public const string TutorialGetTutorials = "TUTORIAL_GET_TUTORIALS";
    public const string TutorialTutorials = "TUTORIAL_TUTORIALS";
    public const string TutorialMarkTaskDone = "TUTORIAL_MARK_TASK_DONE";
    public const string TutorialTourMarkFinished = "TUTORIAL_TOUR_MARK_FINISHED";
    public const string TutorialTourMarkSkipped = "TUTORIAL_TOUR_MARK_SKIPPED";
    public const string TutorialTourStart = "TUTORIAL_TOUR_START";
    public const string TutorialTourStop = "TOUR_STOP";
    public const string TutorialTourReset = "TOUR_RESET";
    public const string TutorialTourTour = "TUTORIAL_TOUR_TOUR";
    public const string TutorialTourSetupStep = "TUTORIAL_TOUR_SETUP_STEP";
    public const string TutorialTourIncreaseStep = "TUTORIAL_TOUR_INCREASE_STEP";
    public const string TutorialTourDecreaseStep = "TUTORIAL_TOUR_DECREASE_STEP";
    public const string TutorialTourStepConditionFulfilled = "TUTORIAL_TOUR_STEP_CONDITION_FULFILLED";
    public const string TutorialHintRequestTriggerHint = "TUTORIAL_HINT_REQUEST_TRIGGER_HINT";
    public const string TutorialHintMarkHintTriggered = "TUTORIAL_HINT_MARK_HINT_TRIGGERED";
    public const string TutorialHintToggleHints = "TUTORIAL_HINT_TOGGLE_HINTS";
    
    /* UI */
    public const string UiGetData = "UI_GET_DATA";
    public const string UiData = "UI_DATA";
    
    /* UI: Audio */
    public const string UiAudioSetEnabled = "UI_AUDIO_SET_ENABLED";
    
    /* UI: Buffers */
    public const string UiBuffersCreate = "UI_BUFFERS_CREATE";
    
    /* UI: Fullscreen */
    public const string UiFullscreenToggle = "UI_FULLSCREEN_TOGGLE";
    public const string UiFullscreenChanged = "UI_FULLSCREEN_CHANGED";
    
    /* UI: Hash */
    public const string UiHashChanged = "UI_HASH_CHANGED";
    
    /* UI: Help */
    public const string UiHelpSetEnabled = "UI_HELP_SET_ENABLED";
    public const string UiContextHelpSetEnabled = "UI_CONTEXT_HELP_SET_ENABLED";
    
    /* UI: on screen keyboard */
    public const string UiSetOnScreenKeyboardVisibility = "UI_SET_ON_SCREEN_KEYBOARD_VISIBILITY";
    
    /* UI: Screens */
    public const string UiScreensAdd = "UI_SCREENS_ADD";
    public const string UiScreensDelete = "UI_SCREENS_DELETE";
    public const string UiScreensRename = "UI_SCREENS_RENAME";
    public const string UiScreensUndelete = "UI_SCREENS_UNDELETE";
    public const string UiScreensSetState = "UI_SCREENS_SET_STATE";
    
    /* UI: Stack */
    public const string UiCardsAdd = "UI_CARDS_ADD";
    public const string UiCardsGet = "UI_CARDS_GET";
    public const string UiCardsDelete = "UI_CARDS_DELETE";
    public const string UiCardsMove = "UI_CARDS_MOVE";
    public const string UiCardsCards = "UI_CARDS_CARDS";
    public const string UiCardsCard = "UI_CARDS_CARD";
    public const string UiStacksAdd = "UI_STACKS_ADD";
    public const string UiStacksGet = "UI_STACKS_GET";
    public const string UiStacksDelete = "UI_STACKS_DELETE";
    public const string UiStacksStacks = "UI_STACKS_STACKS";
    public const string UiStacksStack = "UI_STACKS_STACK";
    
    /* UI: Tiling */
    public const string UiTilesChangeCommand = "UI_TILES_CHANGE_COMMAND";
    public const string UiTilesChangeSize = "UI_TILES_CHANGE_SIZE";
    public const string UiTilesMove = "UI_TILES_MOVE";
    public const string UiTilesRemove = "UI_TILES_REMOVE";
    public const string UiTilesSplit = "UI_TILES_SPLIT";
    public const string UiTilesSetState = "UI_TILES_SET_STATE";
    public const string UiTilesEnableControls = "UI_TILES_ENABLE_CONTROLS";
    public const string UiTilesDisableControls = "UI_TILES_DISABLE_CONTROLS";
    
    /* UI: Windows */
    public const string UiWindowsCreate = "UI_WINDOWS_CREATE";
    public const string UiWindowsOpen = "UI_WINDOWS_OPEN";
    public const string UiWindowsClose = "UI_WINDOWS_CLOSE";
    public const string UiWindowsCloseAll = "UI_WINDOWS_CLOSE_ALL";
    public const string UiWindowsUpdatePosition = "UI_WINDOWS_UPDATE_POSITION";
    public const string UiWindowsUpdateSize = "UI_WINDOWS_UPDATE_SIZE";
    public const string UiWindowsRequestFocus = "UI_WINDOWS_REQUEST_FOCUS";
    public const string UiWindowsDisableControls = "UI_WINDOWS_DISABLE_CONTROLS";
    public const string UiWindowsEnableControls = "UI_WINDOWS_ENABLE_CONTROLS";
    
    /* User */
    public const string UserGetData = "USER_GET_DATA";
    public const string UserData = "USER_DATA";
    public const string UserGetResetData = "USER_GET_RESET_DATA";
    public const string UserResetData = "USER_RESET_DATA";
    public const string UserGetStartingProfileData = "USER_GET_STARTING_PROFILE_DATA";
    public const string UserStartingProfileData = "USER_STARTING_PROFILE_DATA";
    public const string UserGetStartingLocationData = "USER_GET_STARTING_LOCATION_DATA";
    public const string UserStartingLocationData = "USER_STARTING_LOCATION_DATA";
    public const string UserSelectStartingProfile = "USER_SELECT_STARTING_PROFILE";
    public const string UserSelectStartingLocation = "USER_SELECT_STARTING_LOCATION";
    public const string UserSelectCompanyRegistration = "USER_SELECT_COMPANY_USER_SELECT_COMPANY_REGISTRATION";
    public const string UserAcceptDisclaimer = "USER_ACCEPT_DISCLAIMER";
    public const string UserFoundCompany = "USER_FOUND_COMPANY";
    public const string UserDeleteCompany = "USER_DELETE_COMPANY";
    public const string UserDeleteCompanyConfirmed = "USER_DELETE_COMPANY_CONFIRMED";
    public const string UserQueryUserList = "USER_QUERY_USER_LIST";
    public const string UserUserList = "USER_USER_LIST";
    public const string UserMuteUser = "USER_MUTE_USER";
    public const string UserUnmuteUser = "USER_UNMUTE_USER";
    
    /* Warehouse */
    public const string WarehouseGetStorages = "WAREHOUSE_GET_STORAGES";
    public const string WarehouseStorages = "WAREHOUSE_STORAGES";
    public const string WarehouseStorage = "WAREHOUSE_STORAGE";
    public const string WarehouseRentUnit = "WAREHOUSE_RENT_UNIT";
    public const string WarehouseCancelUnit = "WAREHOUSE_CANCEL_UNIT";
    public const string WarehouseStorageRemoved = "WAREHOUSE_STORAGE_REMOVED";
    
    /* Workforces */
    public const string WorkforceFindWorkforces = "WORKFORCE_FIND_WORKFORCES";
    public const string WorkforceWorkforces = "WORKFORCE_WORKFORCES";
    public const string WorkforceWorkforcesUpdated = "WORKFORCE_WORKFORCES_UPDATED";
    
    /* World Data */
    public const string WorldGetSectors = "WORLD_GET_SECTORS";
    public const string WorldSectors = "WORLD_SECTORS";
    public const string WorldGetMaterialCategories = "WORLD_GET_MATERIAL_CATEGORIES";
    public const string WorldMaterialCategories = "WORLD_MATERIAL_CATEGORIES";
    public const string WorldFindMaterialData = "WORLD_FIND_MATERIAL_DATA";
    public const string WorldMaterialData = "WORLD_MATERIAL_DATA";
    public const string WorldFindReactorData = "WORLD_FIND_REACTOR_DATA";
    public const string WorldReactorData = "WORLD_REACTOR_DATA";
}
